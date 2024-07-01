using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using OfficeBaseApp.Components.CsvReader;
using OfficeBaseApp.Components.CsvReader.Models;
using OfficeBaseApp.Components.DataProviders;
using OfficeBaseApp.Data;
using OfficeBaseApp.Data.Entities;
using OfficeBaseApp.Data.Repositories;
using OfficeBaseApp.Data.Repositories.Extensions;
using System.Xml.Linq;
using System.Xml.XPath;
using static OfficeBaseApp.TextMenu;

namespace OfficeBaseApp;
public class App : IApp
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Vendor> _vendorRepository;
    private readonly IRepository<Component> _componentRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IComponentProvider _componentProvider;
    private readonly ICsvReader _csvReader;

    public App(IRepository<Customer> customerRepository,
               IRepository<Vendor> vendorRepository,
               IRepository<Component> componentRepository,
               IRepository<Product> productRepository,
               IComponentProvider componentProvider,
               ICsvReader csvReader)
    {
        _customerRepository = customerRepository;
        _customerRepository.SetUp();
        _vendorRepository = vendorRepository;
        _vendorRepository.SetUp();
        _componentRepository = componentRepository;
        _componentRepository.SetUp();
        _productRepository = productRepository;
        _productRepository.SetUp();
        _componentProvider = componentProvider;
        _csvReader = csvReader;
    }
    public void Run()
    {
        CreateOutputXml();
        
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
        //    var menuItems = new List<string>() { "<-Exit", "Create/restore SQL Base & seeded sample data", "Customers", "Vendors", "Components", "Products" };
        //    var menuActionMap = new List<TextMenu.MenuItemAction>()
        //    {
        //        () => {PrintHeader(); Environment.Exit(0);},
        //        () => ResetDatabase(_customerRepository,_vendorRepository,_componentRepository, _productRepository, menuItems.Count + 6),
        //          () => {CommonSubmenuOptionsForRepositories<Customer>(_customerRepository);},
        //              () => {CommonSubmenuOptionsForRepositories<Vendor>(_vendorRepository);},
        //              () => {CommonSubmenuOptionsForRepositories<Component>(_componentRepository);},
        //              () => {CommonSubmenuOptionsForRepositories<Product>(_productRepository); },
        //    };
        //    new TextMenu(menuItems, menuActionMap).Run();
    }

    public void CreateOutputXml()
    {
        var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");
        var manufacturers = _csvReader.ProcessManufacturers("Resources\\Files\\manufacturers.csv");
        var groups = manufacturers.GroupJoin(
            cars,
            manufacturer => manufacturer.Name,
            car => car.Manufacturer,
            (m, g) => new
            {
                Manufacturer = m,
                Cars = g
            })
            .OrderBy(x => x.Manufacturer.Name);
        var document = new XDocument(new XElement("Manufacturers"));
        foreach (var group in groups)
        {
            var element = new XElement("Manufacturer",
                                new XAttribute("Name", group.Manufacturer.Name),
                                new XAttribute("Country", group.Manufacturer.Country),
                                new XElement("Cars",
                                new XAttribute("Country", group.Manufacturer.Country),
                                new XAttribute("CombinedSum", group.Cars.Sum(x => x.Combined)),
                                group.Cars .OrderByDescending(x => x.Combined)
                                           .Select(x => new XElement("Car",
                                                       new XAttribute("Model", x.Name),
                                                       new XAttribute("Combined", x.Combined)))                                       
                                ));
            document.Root.Add(element);
        }
        document.Save("Output.xml");
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

    //public void CommonSubmenuOptionsForRepositories<T>(IRepository<T> repo) where T : class, IEntity, new()
    //{
    //    var menuItems = new List<string> { "<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest" };
    //    var menuActionMap = new List<MenuItemAction>()
    //                        {() => {},
    //                         () => LoadRepository<T>(repo, menuItems.Count + 6),
    //                         () => PrintRepository<T>(repo, menuItems.Count + 6),
    //                         () => {
    //                                PrintRepository(repo, menuItems.Count + 6);
    //                                var item = new T();
    //                                item.EnterPropertiesFromConsole();
    //                                repo.Add(item);
    //                                PrintRepository<T>(repo, menuItems.Count + 6);
    //                                WaitTillKeyPressed(true);
    //                               },
    //                         () => {RemoveItemFromRepository<T>(repo, menuItems.Count + 6);},
    //                         () => {
    //                                Console.SetCursorPosition(0, menuItems.Count + 6);
    //                                ComponentProviderTest();
    //                               }
    //                         };
    //    new TextMenu(menuItems, menuActionMap).Run();
    //}
    //public void LoadRepository<T>(IRepository<T> repo, int position) where T : class, IEntity, new()
    //{
    //    Console.SetCursorPosition(0, position);
    //    IRepository<T> repository = repo;
    //    repository.Load();
    //    WaitTillKeyPressed(true);
    //}
    //public void PrintRepository<T>(IRepository<T> repo, int position) where T : class, IEntity, new()
    //{
    //    Console.SetCursorPosition(0, position);
    //    repo.WriteAllToConsole();
    //}
    //public void RemoveItemFromRepository<T>(IRepository<T> repo, int position) where T : class, IEntity, new()
    //{
    //    PrintRepository(repo, position);
    //    Console.WriteLine();
    //    Console.WriteLine($"Remove {typeof(T).Name} from repository {repo.GetType().Name.Remove(repo.GetType().Name.Length - 2)}");
    //    Console.WriteLine("Enter the item's name or Id number: ");
    //    Console.CursorVisible = true;
    //    var itemName = Console.ReadLine();
    //    Console.CursorVisible = false;
    //    if (itemName != null)
    //    {
    //        if (repo.GetItem(itemName) != null)
    //        {
    //            repo.Remove(repo.GetItem(itemName));
    //        }
    //        else if (int.TryParse(itemName, out var itemNumber))
    //        {
    //            if (repo.GetItem(itemNumber) != null)
    //            {
    //                repo.Remove(repo.GetItem(itemNumber));
    //            }
    //            else
    //            {
    //                Console.WriteLine("Sorry, there's no item with such an Id.");
    //            }
    //        }
    //        else
    //        {
    //            Console.WriteLine("Sorry, there's no item with such a name nor Id.");
    //        }
    //    }
    //    PrintRepository(repo, position);
    //    WaitTillKeyPressed(true);
    //}
    //public void WaitTillKeyPressed(bool clearConsole)
    //{
    //    Console.WriteLine();
    //    Console.WriteLine("<Press any key to continue>");
    //    Console.ReadKey();
    //    if (clearConsole)
    //    {
    //        PrintHeader();
    //    }
    //}
    //public void ResetDatabase(IRepository<Customer> customerRepo, IRepository<Vendor> vendorRepo, IRepository<Component> componentRepo, IRepository<Product> productRepo, int position)                                        //Initialize Json files with sample data                
    //{
    //    Console.SetCursorPosition(0, position);
    //    Console.ForegroundColor = ConsoleColor.Blue;
    //    Console.WriteLine("==> Deleting old SQL Base and init sample data again.");
    //    Console.ForegroundColor = ConsoleColor.Gray;
    //    Console.WriteLine();
    //    DatabaseInit.Initialize();
    //    InitSampleData.AddCustomers(customerRepo);
    //    InitSampleData.AddVendors(vendorRepo);
    //    InitSampleData.AddComponents(componentRepo);
    //    InitSampleData.AddProducts(productRepo);
    //    Console.WriteLine("Database seeded with demo data.");
    //    WaitTillKeyPressed(true);
    //}
    //public void ComponentProviderTest()
    //{
    //    Console.WriteLine("-------------------------------------------------");
    //    Console.WriteLine("Get Unique Names");
    //    var items = _componentProvider.GetUniqueNames();
    //    foreach (var item in items)
    //    {
    //        Console.WriteLine(item);
    //    }
    //    WaitTillKeyPressed(false);
    //    Console.WriteLine("-------------------------------------------------");
    //    Console.WriteLine("Minimum Price");
    //    Console.WriteLine($"Minimum price of all Components {_componentProvider.GetMinimumPriceOfAllComponents()}");
    //    WaitTillKeyPressed(false);

    //    Console.WriteLine("-------------------------------------------------");
    //    Console.WriteLine("Get specific columns from all Components:");
    //    var items1 = _componentProvider.GetSpecificColumns();
    //    foreach (var item in items1)
    //    {
    //        Console.WriteLine(item);
    //    }
    //    WaitTillKeyPressed(false);

    //    Console.WriteLine("-------------------------------------------------");
    //    Console.WriteLine("Anonymous Class:");
    //    Console.WriteLine(_componentProvider.AnonymousClass());
    //    WaitTillKeyPressed(false);

    //    Console.WriteLine("-------------------------------------------------");
    //    Console.WriteLine("Components sorted by Name:");
    //    var items2 = _componentProvider.OrderByName();
    //    foreach (var item in items2)
    //    {
    //        Console.WriteLine(item);
    //    }
    //    WaitTillKeyPressed(false);

    //    Console.WriteLine("-------------------------------------------------");
    //    Console.WriteLine("Components sorted by Name, descending:");
    //    var items3 = _componentProvider.OrderByNameDescending();
    //    foreach (var item in items3)
    //    {
    //        Console.WriteLine(item);
    //    }

    //    Console.WriteLine("-------------------------------------------------");
    //    Console.WriteLine("Components sorted by Price and then by Name:");
    //    var items4 = _componentProvider.OrderByNameAndPrice();
    //    foreach (var item in items4)
    //    {
    //        Console.WriteLine(item);
    //    }
    //    WaitTillKeyPressed(true);
    //}
}