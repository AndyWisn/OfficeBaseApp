using Microsoft.IdentityModel.Tokens;
using OfficeBaseApp.Components.CsvReader;
using OfficeBaseApp.Components.CsvReader.Models;
using OfficeBaseApp.Components.DataProviders;
using OfficeBaseApp.Components.TextMenu;
using OfficeBaseApp.Components.XmlWriter;
using OfficeBaseApp.Data;
using OfficeBaseApp.Data.Entities;
using OfficeBaseApp.Data.Entities.Extensions;
using OfficeBaseApp.Data.Repositories;
using OfficeBaseApp.Data.Repositories.Extensions;
using System.Reflection;
using System.Resources;
using System.Runtime;
using System.Text;
using static OfficeBaseApp.Components.TextMenu.TextMenu;

namespace OfficeBaseApp;
public class App : IApp
{
    private readonly IRepository<Vendor> _vendorRepository;
    private readonly IRepository<ProductionPart> _productionPartRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IProductionPartProvider _productionPartProvider;


    private readonly ICsvReader _csvReader;
    private readonly IXmlWriter _xmlWriter;

    private readonly OfficeBaseAppDbContext _officeBaseAppDbContext;


    private const bool verticalArrowsActive = true;
    private const bool normalEnterAction = true;

    public App(IRepository<Vendor> vendorRepository,
               IRepository<ProductionPart> productionPartRepository,
               IRepository<Product> productRepository,
               IProductionPartProvider productionPartProvider,
               ICsvReader csvReader,
               IXmlWriter xmlWriter,
               OfficeBaseAppDbContext officeBaseAppDbContext)
    {

        _vendorRepository = vendorRepository;
        _vendorRepository.SetUp();
        _productionPartRepository = productionPartRepository;
        _productionPartRepository.SetUp();
        _productRepository = productRepository;
        _productRepository.SetUp();
        _productionPartProvider = productionPartProvider;
        _csvReader = csvReader;
        _xmlWriter = xmlWriter;
        _officeBaseAppDbContext = officeBaseAppDbContext;
        _officeBaseAppDbContext.Database.EnsureCreated();

    }
    public void Run()
    {




        //var carsFromDb = _officeBaseAppDbContext.Cars.ToList();


        //        var cayman = this.ReadFirst("Cayman");
        //        _officeBaseAppDbContext.Cars.Remove(cayman);
        //        _officeBaseAppDbContext.SaveChanges();

        //cayman.Name = "Mój samochód";
        //_officeBaseAppDbContext.SaveChanges();

        //InsertData();
        //ReadAllFromDb();
        //ReadGroupedCarsFromDb();

        //CreateOutputXml();

        //PrintHeader();
        //var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");
        //var manufacturers = _csvReader.ProcessManufacturers("Resources\\Files\\manufacturers.csv");

        //var groups = cars
        //    .GroupBy(x => x.Manufacturer)
        //    .Select(g => new
        //    {
        //        Name = g.Key,
        //        Max = g.Max(c => c.Combined),
        //        Average = g.Average(c => c.Combined)

        //    })
        //    .OrderBy(x => x.Average);

        //foreach(var group in groups)
        //{
        //    Console.WriteLine($"Name: {group.Name}");
        //    Console.WriteLine($"Max: {group.Max}");
        //    Console.WriteLine($"Average: {group.Average}");
        //}

        //var carsInCountry = cars.Join(
        //    manufacturers,
        //    c => new { c.Manufacturer, c.Year },
        //    m => new { Manufacturer = m.Name, m.Year },
        //    (car, manufacturer) =>
        //    new
        //    {
        //        manufacturer.Country,
        //        car.Name,
        //        car.Combined
        //    }
        //    )
        //    .OrderByDescending(x => x.Combined)
        //    .ThenBy(x => x.Name);

        //foreach (var car in carsInCountry)
        //{
        //    Console.WriteLine($"Country: {car.Country}");
        //    Console.WriteLine($"\tName: {car.Name}");
        //    Console.WriteLine($"\tCombined: {car.Combined}");
        //}

        //var groups = manufacturers.GroupJoin(
        //    cars,
        //    manufacturer => manufacturer.Name,
        //    car => car.Manufacturer,
        //    (m, g) =>
        //    new
        //    {
        //        Manufacturer = m,
        //        Cars = g
        //    })
        //    .OrderBy(x => x.Manufacturer.Name);
        //    WaitTillKeyPressed(false);
        //
        //
        Console.Clear();
        PrintHeader();
        var menuItems = new List<string>() { "<-Exit", "Reset Db & read CSV data", "Vendors", "Production Parts", "Products", "Create XML Output" };
        var menuActionMap = new List<TextMenu.MenuItemAction>()
            {
                () => {Console.Clear(); Environment.Exit(0);},
                () => ResetDatabase(_vendorRepository,_productionPartRepository, _productRepository, menuItems.Count + 6),
                      () => {CommonSubmenuOptionsForRepositories<Vendor>(_vendorRepository);},
                      () => {CommonSubmenuOptionsForRepositories<ProductionPart>(_productionPartRepository);},
                      () => {CommonSubmenuOptionsForRepositories<Product>(_productRepository); },
                      () => {Console.Clear();
                              _xmlWriter.CreateOutputXml();
                              WaitTillKeyPressed(); }
            };
        new TextMenu(menuItems, menuActionMap).Run();
    }

    //public Car? ReadFirst(string name)
    //{
    //    return _officeBaseAppDbContext.Cars.FirstOrDefault(x => x.Name == name);
    //}

    public void ReadGroupedCarsFromDb()
    {
        //var groups = _officeBaseAppDbContext
        //            .Cars
        //            .GroupBy(x => x.Manufacturer)
        //            .Select(x => new
        //            {
        //                Name = x.Key,
        //                Cars = x.ToList()

        //            })
        //            .ToList();

        //foreach(var group in groups)
        //{
        //    Console.WriteLine(group.Name);
        //    Console.WriteLine("========");
        //    foreach (var car in group.Cars)
        //    { Console.WriteLine($"\t{car.Name}: {car.Combined}");
        //    }
        //}
    }



    public void CommonSubmenuOptionsForRepositories<T>(IRepository<T> repo) where T : class, IEntity, new()
    {
        (int, int) position = Console.GetCursorPosition();
        Console.SetCursorPosition(0, 3);
        Console.WriteLine($"Repository [{typeof(T).Name}s] holds {repo.GetAll().Count()} items. Page size set to: {repo.printPageSize}");
        Console.SetCursorPosition(position.Item1, position.Item2);

        var menuItems = new List<string> { "<-Back", "Print repository", "New item from Console", "Add batch from CSV", "Setup" };
        var menuActionMap = new List<MenuItemAction>()
                            {() => {},
                             () => {PrintRepositoryInMenuForm(repo);},
                             () => {var item = new T();
                                    item.EnterPropertiesFromConsole();
                                    repo.Add(item);
                                    WaitTillKeyPressed();
                                    PrintHeader();
                                   },
                             () => {Console.WriteLine();
                                    Console.WriteLine("Enter file path & name or hit [Enter] for default path:");
                                    var path = Console.ReadLine();
                                    AddItemsFromCsvFiles<T>(path);
                                    PrintHeader();
                                   },
                             () => {RepositoryMenuSetup(repo);},

                             };
        new TextMenu(menuItems, menuActionMap).Run();
    }

    public void RepositoryMenuSetup<T>(IRepository<T> repo) where T : class, IEntity, new()
    {

        var menuItems = new List<string> { "<-Back", "Set print page size", "Sort By" };
        var menuActionMap = new List<MenuItemAction>()
                            {() => { },
                                () => {
                                    Console.WriteLine();
                                    Console.WriteLine($"Set print page size for [{typeof(T).Name}s] repository:");
                                    Console.CursorVisible = true;
                                    if (int.TryParse(Console.ReadLine(), out int pageSize) && pageSize > 0)
                                    {
                                    repo.printPageSize = pageSize;
                                    Console.WriteLine($"Print page size set to = {pageSize}");
                                    }
                                    else
                                    {
                                    Console.WriteLine("Incorrect page size!");
                                    }
                                    WaitTillKeyPressed();
                                    Console.Clear();
                                    PrintHeader();
                            },
                             () => {Console.WriteLine();
                                    List<string> propName = new List<string>{"Id","Name","Description" };
                                    Console.WriteLine($"0 - sort by {propName[0]}");
                                    Console.WriteLine($"1 - sort by {propName[1]}");
                                    Console.WriteLine($"2 - sort by {propName[2]}");
                                    Console.CursorVisible = true;
                                    repo.sortBy=-1;
                                    do
                                    {
                                        Console.WriteLine();
                                        Console.Write("Enter your chioce: ");
                                        if (int.TryParse(Console.ReadLine(), out int sortBy) && sortBy>=0 && sortBy<3)
                                        {
                                            repo.sortBy = sortBy;
                                            Console.WriteLine();
                                            Console.WriteLine($"[{typeof(T).Name}s] prints sorted by {propName[sortBy]}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect selection. Try again!");
                                        }
                                    }
                                    while(repo.sortBy<0 || repo.sortBy>3);
                                    WaitTillKeyPressed();
                                    Console.Clear();
                                    PrintHeader();},
                            };
        new TextMenu(menuItems, menuActionMap).Run();
    }
    public void PrintRepositoryInMenuForm<T>(IRepository<T> repo) where T : class, IEntity, new()
    {
        ConsoleKeyInfo menuExitKey;
        var activePage = 1;
        var pagesCount = (repo.GetAll().Count() + repo.printPageSize - 1) / repo.printPageSize;
        do
        {
            var items = repo.GetAll()
                            .OrderBy(x => x.Id)
                            .Skip((activePage - 1) * repo.printPageSize)
                            .Take(repo.printPageSize);
            switch (repo.sortBy)
            {
                case 1:
                    items = repo.GetAll()
                                .OrderBy(x => x.Name)
                                .Skip((activePage - 1) * repo.printPageSize)
                                .Take(repo.printPageSize);
                    break;
                case 2:
                    items = repo.GetAll()
                                .OrderBy(x => x.Description)
                                .Skip((activePage - 1) * repo.printPageSize)
                                .Take(repo.printPageSize);
                    break;
            }
            var menuActionMap = new List<MenuItemAction>();
            var menuItems = items.Select(x => x.ToString()).ToList();
            (int, int) position = Console.GetCursorPosition();
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"Page {activePage} of {pagesCount}. Left/Right arrows to jump pages or refresh.");
            Console.SetCursorPosition(position.Item1, position.Item2);
            foreach (var item in items)
            {
                menuActionMap.Add(
                                 () =>
                                  {
                                   (int, int) position = Console.GetCursorPosition();
                                   OnItemContextMenu(repo, item);
                                   Console.SetCursorPosition(position.Item1, position.Item2);
                                  }
                                 );
            }

            menuItems.Insert(0, "<-Back");
            menuActionMap.Insert(0, () => { activePage = 1; });

            menuExitKey = new TextMenu(menuItems, menuActionMap, verticalArrowsActive, normalEnterAction).Run();
            if (menuExitKey.Key == ConsoleKey.LeftArrow)
            {
                if (activePage > 1) activePage--;
            }
            else if (menuExitKey.Key == ConsoleKey.RightArrow)
            {
                if (activePage < pagesCount) activePage++;
            }

        }
        while ((menuExitKey.Key == ConsoleKey.LeftArrow) ^ (menuExitKey.Key == ConsoleKey.RightArrow));
    }
    public void WaitTillKeyPressed()
    {
        Console.CursorVisible = false;
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
    }
    public void ResetDatabase(IRepository<Vendor> vendorRepo, IRepository<ProductionPart> componentRepo, IRepository<Product> productRepo, int position)                                        //Initialize Json files with sample data                
    {
        Console.SetCursorPosition(0, position);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==> Deleting old SQL Base and reding data from: Vendros.csv, ProductionParts.csv, Products.csv");
        _officeBaseAppDbContext.Database.EnsureDeleted();
        _officeBaseAppDbContext.Database.EnsureCreated();
        var filePath = "Resources\\Files\\Vendors.csv";
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"No file at this path: {filePath}");
        }
        else
        {
            ReadVendorsFromCsvFile(filePath);
        }
        filePath = "Resources\\Files\\ProductionParts.csv";
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"No file at this path: {filePath}");
        }
        else
        {
            ReadProductionPartsFromCsvFile(filePath);
        }
        filePath = "Resources\\Files\\Products.csv";
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"No file at this path: {filePath}");
        }
        else
        {
            ReadProductsFromCsvFile(filePath);
        }
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine();
        Console.WriteLine("Database seeded with demo data.");
        WaitTillKeyPressed();
    }
    public void ComponentProviderTest()
    {
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Get Unique Names");
        var items = _productionPartProvider.GetUniqueNames();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed();
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Minimum Price");
        Console.WriteLine($"Minimum price of all Components {_productionPartProvider.GetMinimumPriceOfAllParts()}");
        WaitTillKeyPressed();

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Get specific columns from all Components:");
        var items1 = _productionPartProvider.GetSpecificColumns();
        foreach (var item in items1)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed();

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Anonymous Class:");
        Console.WriteLine(_productionPartProvider.AnonymousClass());
        WaitTillKeyPressed();

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Name:");
        var items2 = _productionPartProvider.OrderByName();
        foreach (var item in items2)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed();

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Name, descending:");
        var items3 = _productionPartProvider.OrderByNameDescending();
        foreach (var item in items3)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Price and then by Name:");
        var items4 = _productionPartProvider.OrderByNameAndPrice();
        foreach (var item in items4)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed();
    }
    private void ReadVendorsFromCsvFile(string path)
    {
        var vendors = _csvReader.ProcessVendors(path);
        foreach (var vendor in vendors)
        {
            _vendorRepository.Add(new Vendor()
            {
                Name = vendor.Name,
                Contact = vendor.Contact,
                Description = vendor.Description,
                Country = vendor.Country,
                VendorCertificate = vendor.VendorCertificate,

            });
        }
        _officeBaseAppDbContext.SaveChanges();
    }
    private void ReadProductionPartsFromCsvFile(string path)
    {
        var productionParts = _csvReader.ProcessProductionParts(path);
        foreach (var productionPart in productionParts)
        {
            _productionPartRepository.Add(new ProductionPart(productionPart.Name,
                                                             productionPart.Price,
                                                             productionPart.Description,
                                                             productionPart.PartVendor,
                                                             productionPart.PartManufacturer));
        }
        _officeBaseAppDbContext.SaveChanges();
    }

    private void ReadProductsFromCsvFile(string path)
    {
        var products = _csvReader.ProcessProducts(path);
        foreach (var product in products)
        {
            _productRepository.Add(new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            });
        }
        _officeBaseAppDbContext.SaveChanges();
    }
    private void AddItemsFromCsvFiles<T>(string path) where T : class, IEntity, new()
    {
        var _path = path;
        Type typeOfT = typeof(T);
        if (!File.Exists(_path))
        {
            Console.WriteLine("No such a filename at this path!");
        }
        else if (typeOfT == typeof(Product))
        {
            if (_path.IsNullOrEmpty()) { _path = "Resources\\Files\\Products_batch.csv"; }
            Console.WriteLine(_path);
            ReadProductsFromCsvFile(_path);
        }
        else if (typeOfT == typeof(ProductionPart))
        {
            if (_path.IsNullOrEmpty()) { _path = "Resources\\Files\\ProductionParts_batch.csv"; }
            Console.WriteLine(_path);
            ReadProductionPartsFromCsvFile(_path);
        }
        else if (typeOfT == typeof(Vendor))
        {
            if (_path.IsNullOrEmpty()) { _path = "Resources\\Files\\Vendors_batch.csv"; }
            Console.WriteLine(_path);
            ReadVendorsFromCsvFile(_path);
        }
        else
        {
            throw new NotSupportedException($"Unsupported type: {typeOfT.Name}");
        }
        WaitTillKeyPressed();
    }
    public void OnItemContextMenu<T>(IRepository<T> repo, T item) where T : class, IEntity, new()
    {
        Console.WriteLine();
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine($"Selected item: {item.ToString()}");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine();
        Console.WriteLine("Item's actions menu:");
        var menuItems = new List<string> { "<-Back", "Remove Item", "Modify Item", "Duplicate Item" };
        var menuActionMap = new List<MenuItemAction>()
                            {() => {},
                             () => {repo.Remove(item);},
                             () => {item.EnterPropertiesFromConsole();
                                    repo.Save();
                                    WaitTillKeyPressed();
                                    PrintHeader();
                                   },
                             () => {var newItem = item.Copy();
                                   if (newItem != null)
                                   {newItem.Id = 0;
                                    repo.Add(newItem);
                                   };
                                  },
                            };
        new TextMenu(menuItems, menuActionMap, !verticalArrowsActive, !normalEnterAction).Run();
    }
}