using Microsoft.IdentityModel.Tokens;
using OfficeBaseApp.Components.CsvReader;
using OfficeBaseApp.Components.DataProviders;
using OfficeBaseApp.Components.TextMenu;
using OfficeBaseApp.Data;
using OfficeBaseApp.Data.Entities;
using OfficeBaseApp.Data.Entities.Extensions;
using OfficeBaseApp.Data.Repositories;
using OfficeBaseApp.Data.Repositories.Extensions;
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

    private readonly OfficeBaseAppDbContext _officeBaseAppDbContext;

    private const bool verticalArrowsActive = true;
    private const bool normalEnterAction = true;

    public App(IRepository<Vendor> vendorRepository,
               IRepository<ProductionPart> productionPartRepository,
               IRepository<Product> productRepository,
               IProductionPartProvider productionPartProvider,
               ICsvReader csvReader,
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
        var menuItems = new List<string>() { "<-Exit", "Reset Db & read CSV data", "Vendors", "Components", "Products" };
        var menuActionMap = new List<TextMenu.MenuItemAction>()
            {
                () => {Console.Clear(); Environment.Exit(0);},
                () => ResetDatabase(_vendorRepository,_productionPartRepository, _productRepository, menuItems.Count + 6),
                      () => {CommonSubmenuOptionsForRepositories<Vendor>(_vendorRepository);},
                      () => {CommonSubmenuOptionsForRepositories<ProductionPart>(_productionPartRepository);},
                      () => {CommonSubmenuOptionsForRepositories<Product>(_productRepository); },
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

    public void ReadAllFromDb()
    {
        //var carsFromDb = _officeBaseAppDbContext.Cars.ToList();
        //foreach (var carFromDb in carsFromDb)
        //{
        //    Console.WriteLine($"\t{carFromDb.Name}: {carFromDb.Combined}");
        //}
    }
    public void CreateOutputXml()
    {
        //var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");
        //var manufacturers = _csvReader.ProcessManufacturers("Resources\\Files\\manufacturers.csv");
        //var groups = manufacturers.GroupJoin(
        //    cars,
        //    manufacturer => manufacturer.Name,
        //    car => car.Manufacturer,
        //    (m, g) => new
        //    {
        //        Manufacturer = m,
        //        Cars = g
        //    })
        //    .OrderBy(x => x.Manufacturer.Name);
        //var document = new XDocument(new XElement("Manufacturers"));
        //foreach (var group in groups)
        //{
        //    var element = new XElement("Manufacturer",
        //                        new XAttribute("Name", group.Manufacturer.Name),
        //                        new XAttribute("Country", group.Manufacturer.Country),
        //                        new XElement("Cars",
        //                        new XAttribute("Country", group.Manufacturer.Country),
        //                        new XAttribute("CombinedSum", group.Cars.Sum(x => x.Combined)),
        //                        group.Cars.OrderByDescending(x => x.Combined)
        //                                   .Select(x => new XElement("Car",
        //                                               new XAttribute("Model", x.Name),
        //                                               new XAttribute("Combined", x.Combined)))
        //                        ));
        //    document.Root.Add(element);
        //}
        //document.Save("Output.xml");
    }

    //public void QueryXml()
    //{
    //    var document = XDocument.Load("fuel.xml");
    //    var names = document
    //        .Element("Cars")?
    //        .Elements("Car")
    //        .Where(x => x.Attribute("Manufacturer")?.Value == "BMW")
    //        .Select(x => x.Attribute("Name")?.Value);
    //    foreach (var name in names)
    //    { Console.WriteLine(name); }
    //}

    //public void CreateXml()
    //{
    //    var records = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");
    //    var document = new XDocument();
    //    var cars = new XElement("Cars", records
    //                                     .Select(x =>
    //                                     new XElement("Car",
    //                                     new XAttribute("Name", x.Name),
    //                                     new XAttribute("Combined", x.Combined),
    //                                     new XAttribute("Manufacturer", x.Manufacturer))
    //                                            )
    //                            );

    //    document.Add(cars);
    //    document.Save("fuel.xml");
    //}

    public void CommonSubmenuOptionsForRepositories<T>(IRepository<T> repo) where T : class, IEntity, new()
    {
       
        
        var menuItems = new List<string> { "<-Back", "Print repository","New item from Console", "Add batch from CSV", "Export to XML"};
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
                             () => {},
 
                             };
        new TextMenu(menuItems, menuActionMap).Run();
    }

    public void PrintRepositoryInMenuForm<T>(IRepository<T> repo) where T : class, IEntity, new()
    {

        ConsoleKeyInfo menuExitKey;
        var pageSize = 10;
            var activePage = 1;
            var pagesCount = (repo.GetAll().Count() + pageSize - 1) / pageSize;

            do
            {
                var items = repo.GetAll()
                                .Skip((activePage - 1) * pageSize)
                                .Take(pageSize);

                var menuActionMap = new List<MenuItemAction>();
                var menuItems = items.Select(x => x.ToString()).ToList();

                (int, int) position = Console.GetCursorPosition();
                Console.SetCursorPosition(0, 3);
                Console.WriteLine($"Page {activePage} of {pagesCount}. Left/Right arrows to jump pages or refresh.");
                Console.SetCursorPosition(position.Item1, position.Item2);

                foreach (var item in items)
                {
                    menuActionMap.Add(() => {
                        (int, int) position = Console.GetCursorPosition();
                        OnItemContextMenu(repo,item);
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
    
    public void RemoveItemFromRepository<T>(IRepository<T> repo, int position) where T : class, IEntity, new()
    {
        PrintRepositoryInMenuForm(repo);
        Console.WriteLine();
        Console.WriteLine($"Remove {typeof(T).Name} from repository {repo.GetType().Name.Remove(repo.GetType().Name.Length - 2)}");
        Console.WriteLine("Enter the item's name or Id number: ");
        Console.CursorVisible = true;
        var itemName = Console.ReadLine();
        Console.CursorVisible = false;
        if (itemName != null)
        {
            if (repo.GetItem(itemName) != null)
            {
                repo.Remove(repo.GetItem(itemName));
            }
            else if (int.TryParse(itemName, out var itemNumber))
            {
                if (repo.GetItem(itemNumber) != null)
                {
                    repo.Remove(repo.GetItem(itemNumber));
                }
                else
                {
                    Console.WriteLine("Sorry, there's no item with such an Id.");
                }
            }
            else
            {
                Console.WriteLine("Sorry, there's no item with such a name nor Id.");
            }
        }
        PrintRepositoryInMenuForm(repo);
        WaitTillKeyPressed();
    }
    public void WaitTillKeyPressed()
    {
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
        //if (clearConsole)
        //{
        //    PrintHeader();
        //}
    }
    public void ResetDatabase(IRepository<Vendor> vendorRepo, IRepository<ProductionPart> componentRepo, IRepository<Product> productRepo, int position)                                        //Initialize Json files with sample data                
    {
        Console.SetCursorPosition(0, position);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==> Deleting old SQL Base and reding data from: Vendros.csv, ProductionParts.csv, Products.csv");
        _officeBaseAppDbContext.Database.EnsureDeleted();
        _officeBaseAppDbContext.Database.EnsureCreated();
        ReadVendorsFromCsvFile("Resources\\Files\\Vendors.csv");
        ReadProductionPartsFromCsvFile("Resources\\Files\\ProductionParts.csv");
        ReadProductsFromCsvFile("Resources\\Files\\Products.csv");
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

    public static void Initialize(OfficeBaseAppDbContext _officeBaseAppDbContext)
    {
        if (_officeBaseAppDbContext.Database.CanConnect())
        {
            Console.WriteLine("Database exists.");
            _officeBaseAppDbContext.Database.EnsureDeleted();
            Console.WriteLine("Database deleted.");
        }
        _officeBaseAppDbContext.Database.EnsureCreated();
        Console.WriteLine("New Database created.");
        _officeBaseAppDbContext.SaveChanges();
        Console.WriteLine("Database seeded with demo data.");

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
                SupportContact = vendor.SupportContact,
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
                                                             productionPart.PartVendor,
                                                             productionPart.Description,
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
        if (typeOfT == typeof(Product))
        {
            if(_path.IsNullOrEmpty()) {_path = "Resources\\Files\\Products_batch.csv";}
            Console.WriteLine(_path);
            ReadProductsFromCsvFile(_path);
        }
        else if (typeOfT == typeof(ProductionPart))
        {
            if (_path.IsNullOrEmpty()) { _path = "Resources\\Files\\ProductionParts_batch.csv";}
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
        Console.WriteLine(item.ToString());
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine();
        Console.WriteLine("Item's actions menu:");
        var menuItems = new List<string> { "<-Back", "Remove Item", "Modify Item", "Duplicate Item" };
        var menuActionMap = new List<MenuItemAction>()
                            {() => {},
                             () => {repo.Remove(item);},
                             () => {},                                    
                             () => {var newItem = item.Copy();
                                   if (newItem != null)
                                   {newItem.Id = 0;
                                    repo.Add(newItem);                                   
                                   };
                                  },
                            };
        new TextMenu(menuItems, menuActionMap,!verticalArrowsActive,!normalEnterAction).Run();
    }
}