using OfficeBaseApp.Data;
using OfficeBaseApp.DataProviders;
using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Repositories.Extensions;
using static OfficeBaseApp.TextMenu;

namespace OfficeBaseApp;
public class App : IApp
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Vendor> _vendorRepository;
    private readonly IRepository<Component> _componentRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IComponentProvider _componentProvider;

    public App(IRepository<Customer> customerRepository,
               IRepository<Vendor> vendorRepository,
               IRepository<Component> componentRepository,
               IRepository<Product> productRepository,
               IComponentProvider componentProvider)
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
    }
    public void Run()
    {
        PrintHeader();
        var menuItems = new List<string>() { "<-Exit", "Create/restore SQL Base & seeded sample data", "Customers", "Vendors", "Components", "Products" };
        var menuActionMap = new List<TextMenu.MenuItemAction>()
        {
            () => {PrintHeader(); Environment.Exit(0);},
            () => ResetDatabase(_customerRepository,_vendorRepository,_componentRepository, _productRepository, menuItems.Count + 6),
              () => {CommonSubmenuOptionsForRepositories<Customer>(_customerRepository);},
                  () => {CommonSubmenuOptionsForRepositories<Vendor>(_vendorRepository);},
                  () => {CommonSubmenuOptionsForRepositories<Component>(_componentRepository);},
                  () => {CommonSubmenuOptionsForRepositories<Product>(_productRepository); },
        };
        new TextMenu(menuItems, menuActionMap).Run();
    }
    public void CommonSubmenuOptionsForRepositories<T>(IRepository<T> repo) where T : class, IEntity, new()
    {
        var menuItems = new List<string> { "<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest" };
        var menuActionMap = new List<MenuItemAction>()
                            {() => {},
                             () => LoadRepository<T>(repo, menuItems.Count + 6),
                             () => PrintRepository<T>(repo, menuItems.Count + 6),
                             () => {
                                    PrintRepository(repo, menuItems.Count + 6);
                                    var item = new T();
                                    item.EnterPropertiesFromConsole();
                                    repo.Add(item);
                                    PrintRepository<T>(repo, menuItems.Count + 6);
                                    WaitTillKeyPressed(true);
                                   },
                             () => {RemoveItemFromRepository<T>(repo, menuItems.Count + 6);},
                             () => {
                                    Console.SetCursorPosition(0, menuItems.Count + 6);
                                    ComponentProviderTest();
                                   }
                             };
        new TextMenu(menuItems, menuActionMap).Run();
    }
    public void LoadRepository<T>(IRepository<T> repo, int position) where T : class, IEntity, new()
    {
        Console.SetCursorPosition(0, position);
        IRepository<T> repository = repo;
        repository.Load();
        WaitTillKeyPressed(true);
    }
    public void PrintRepository<T>(IRepository<T> repo, int position) where T : class, IEntity, new()
    {
        Console.SetCursorPosition(0, position);
        repo.WriteAllToConsole();
    }
    public void RemoveItemFromRepository<T>(IRepository<T> repo, int position) where T : class, IEntity, new()
    {
        PrintRepository(repo, position);
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
        PrintRepository(repo, position);
        WaitTillKeyPressed(true);
    }
    public void WaitTillKeyPressed(bool clearConsole)
    {
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
        if (clearConsole)
        {
            PrintHeader();
        }
    }
    public void ResetDatabase(IRepository<Customer> customerRepo, IRepository<Vendor> vendorRepo, IRepository<Component> componentRepo, IRepository<Product> productRepo, int position)                                        //Initialize Json files with sample data                
    {
        Console.SetCursorPosition(0, position);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==> Deleting old SQL Base and init sample data again.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine();
        DatabaseInit.Initialize();
        InitSampleData.AddCustomers(customerRepo);
        InitSampleData.AddVendors(vendorRepo);
        InitSampleData.AddComponents(componentRepo);
        InitSampleData.AddProducts(productRepo);
        Console.WriteLine("Database seeded with demo data.");
        WaitTillKeyPressed(true);
    }
    public void ComponentProviderTest()
    {
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Get Unique Names");
        var items = _componentProvider.GetUniqueNames();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(false);
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Minimum Price");
        Console.WriteLine($"Minimum price of all Components {_componentProvider.GetMinimumPriceOfAllComponents()}");
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Get specific columns from all Components:");
        var items1 = _componentProvider.GetSpecificColumns();
        foreach (var item in items1)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Anonymous Class:");
        Console.WriteLine(_componentProvider.AnonymousClass());
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Name:");
        var items2 = _componentProvider.OrderByName();
        foreach (var item in items2)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Name, descending:");
        var items3 = _componentProvider.OrderByNameDescending();
        foreach (var item in items3)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Price and then by Name:");
        var items4 = _componentProvider.OrderByNameAndPrice();
        foreach (var item in items4)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(true);
    }
}