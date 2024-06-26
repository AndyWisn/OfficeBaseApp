using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp;
using OfficeBaseApp.Entities;
using OfficeBaseApp.DataProviders;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Repositories.Extensions;
using OfficeBaseApp.Data;

namespace OfficeBaseApp;
public class TextMenu
{
    public delegate void MenuItemAction();
    private List<MenuItemAction> _menuActionMap = new List<MenuItemAction>();
    private List<string> _menuItems = new List<string>();
    private static bool printHeader = true;
    private int previousWidth = Console.WindowWidth;
    private int previousHeight = Console.WindowHeight;

    private readonly IListRepository<Customer> _customerListRepository;
    private readonly IListRepository<Vendor> _vendorListRepository;
    private readonly IListRepository<Component> _componentListRepository;
    private readonly IListRepository<Product> _productListRepository;
    private readonly ISqlRepository<Customer> _customerSqlRepository;
    private readonly ISqlRepository<Vendor> _vendorSqlRepository;
    private readonly ISqlRepository<Component> _componentSqlRepository;
    private readonly ISqlRepository<Product> _productSqlRepository;
    private readonly IComponentProviderList _componentProviderList;
    private readonly IComponentProviderSql _componentProviderSql;

    public TextMenu(IListRepository<Customer> customerRepository,
                    IListRepository<Vendor> vendorRepository,
                    IListRepository<Component> componentRepository,
                    IListRepository<Product> productRepository,
                    ISqlRepository<Customer> customerSqlRepository,
                    ISqlRepository<Vendor> vendorSqlRepository,
                    ISqlRepository<Component> componentSqlRepository,
                    ISqlRepository<Product> productSqlRepository,
                    IComponentProviderList componentProviderList,
                    IComponentProviderSql componentProviderSql,
                    List<string> menuItems,
                    List<MenuItemAction> menuActionMap)
    {
        _customerListRepository = customerRepository;
        _vendorListRepository = vendorRepository;
        _componentListRepository = componentRepository;
        _productListRepository = productRepository;
        _customerSqlRepository = customerSqlRepository;
        _vendorSqlRepository = vendorSqlRepository;
        _componentSqlRepository = componentSqlRepository;
        _productSqlRepository = productSqlRepository;
        _componentProviderList = componentProviderList;
        _componentProviderSql = componentProviderSql;

        _menuItems = menuItems;
        _menuActionMap = menuActionMap;

    }
    public void Run()
    {
        var actualItem = 1;
        Console.CursorVisible = false;
        printHeader = true;
        bool menuIsActive = true;
        while (menuIsActive)
        {
            PrintMenu(actualItem);
            NavigateMenu(ref actualItem, ref menuIsActive);
            _menuActionMap[actualItem].Invoke();
        }
        Console.Clear();
        printHeader = true;
    }
    public void PrintMenu(int actualItem)
    {
        int currentWidth = Console.WindowWidth;
        int currentHeight = Console.WindowHeight;
        if (printHeader)
        {
            PrintHeader();
            printHeader = false;
        }
        if (currentWidth != previousWidth || currentHeight != previousHeight)
        {
            previousWidth = currentWidth;
            previousHeight = currentHeight;
            printHeader = true;
            Console.Clear();
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(0, 2);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, 2);
        Console.WriteLine();
        Console.WriteLine();
        for (int i = 0; i < _menuItems.Count; i++)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            (int, int) position = Console.GetCursorPosition();
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(position.Item1, position.Item2);
            if (i == actualItem)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine(_menuItems[i]);
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void NavigateMenu(ref int actualItem, ref bool menuIsActive)
    {
        do
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if ((keyPressed.Key == ConsoleKey.UpArrow) ^ (keyPressed.Key == ConsoleKey.LeftArrow))
            {
                actualItem = (actualItem > 0) ? actualItem - 1 : _menuItems.Count - 1;
                PrintMenu(actualItem);
            }
            else if ((keyPressed.Key == ConsoleKey.DownArrow) ^ (keyPressed.Key == ConsoleKey.RightArrow))
            {
                actualItem = (actualItem < _menuItems.Count - 1) ? actualItem + 1 : 0;
                PrintMenu(actualItem);
            }
            else if (keyPressed.Key == ConsoleKey.Escape)
            {
                menuIsActive = false;
                actualItem = 0;
                break;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                if (actualItem == 0)
                {
                    menuIsActive = false;
                }
                break;
            }
        }
        while (true);
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
    public void ResetJsonFiles(IListRepository<Customer> customerRepo, IListRepository<Vendor> vendorRepo, IListRepository<Component> componentRepo, IListRepository<Product> productRepo, int position)                                        //Initialize Json files with sample data                
    {
        Console.SetCursorPosition(0, position);
        Console.WriteLine();
        customerRepo.DeleteJsonFiles();
        vendorRepo.DeleteJsonFiles();
        componentRepo.DeleteJsonFiles();
        productRepo.DeleteJsonFiles();
        InitSampleData.AddCustomers(customerRepo);
        InitSampleData.AddVendors(vendorRepo);
        InitSampleData.AddComponents(componentRepo);
        InitSampleData.AddProducts(productRepo);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==> Sample data seeded to ListRepositories. New Json files saved.");
        Console.ForegroundColor = ConsoleColor.Gray;
        WaitTillKeyPressed(true);
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
            printHeader = true;
            Console.Clear();
        }
    }
    public void PrintHeader()
    {
        Console.SetCursorPosition(0, 0);
        Console.Clear();
        Console.WriteLine("============= OfficeBaseApp v.1 Menu Options =============");
        Console.WriteLine("Move up/down with arrows. Confirm with Enter. Esc to exit.");
    }
    public void TextMenu_CommonSubmenuOptionsForRepositories<T>(IRepository<T> repo) where T : class, IEntity, new()
    {
        var menuItems = new List<string> { "<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest" };
        var menuActionMap = new List<MenuItemAction>()
                            {() => { },
                             () => LoadRepository<T>(repo, menuItems.Count + 6),
                             () => PrintRepository<T>(repo, menuItems.Count + 6),
                             () => {
                                 Console.SetCursorPosition(0,menuItems.Count + 6);
                                var item = new T();
                                item.EnterPropertiesFromConsole();
                                repo.Add(item);
                                PrintRepository<T>(repo, menuItems.Count + 6);
                                WaitTillKeyPressed(true);
                                },
                             () => {RemoveItemFromRepository<T>(repo, menuItems.Count + 6);},
                             () => {
                                 Console.SetCursorPosition(0, menuItems.Count + 6);
                                 if(repo is ListRepository<Component> listRepo )
                                 {
                                    ComponentProviderListTest();
                                 }
                                 else
                                 {
                                    ComponentProviderSqlTest();
                                 }
                             }
        };
        new TextMenu(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository,
                                       _customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository,
                                       _componentProviderList, _componentProviderSql,
                                   menuItems, menuActionMap).Run();
    }
    public void TextMenu_HandleSqlRepositorySelectMenu()
    {
        var menuItems = new List<string> { "<-Back", "Customers", "Vendors", "Components", "Products" };
        var menuActionMap = new List<MenuItemAction>()
                { () => {},
                  () => {TextMenu_CommonSubmenuOptionsForRepositories<Customer>(_customerSqlRepository);},
                  () => {TextMenu_CommonSubmenuOptionsForRepositories<Vendor>(_vendorSqlRepository);},
                  () => {TextMenu_CommonSubmenuOptionsForRepositories<Component>(_componentSqlRepository);},
                  () => {TextMenu_CommonSubmenuOptionsForRepositories<Product>(_productSqlRepository);}
                };
        new TextMenu(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository,
                                  _customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository,
                                  _componentProviderList, _componentProviderSql,
                                   menuItems, menuActionMap).Run();
    }
    public void TextMenu_HandleListRepositorySelectMenu()
    {
        var menuItems = new List<string> { "<-Back", "Customers", "Vendors", "Components", "Products" };
        var menuActionMap = new List<MenuItemAction>()
                { () => {},
                  () => {TextMenu_CommonSubmenuOptionsForRepositories<Customer>(_customerListRepository);},
                  () => {TextMenu_CommonSubmenuOptionsForRepositories<Vendor>(_vendorListRepository);},
                  () => {TextMenu_CommonSubmenuOptionsForRepositories<Component>(_componentListRepository);},
                  () => {TextMenu_CommonSubmenuOptionsForRepositories<Product>(_productListRepository); },
                };
        new TextMenu(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository,
                                  _customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository,
                                  _componentProviderList, _componentProviderSql,
                                   menuItems, menuActionMap).Run();
    }
    public void ComponentProviderListTest()
    {
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Get Unique Names");
        var items = _componentProviderSql.GetUniqueNames();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Minimum Price");
        Console.WriteLine($"Minimum price of all Components {_componentProviderSql.GetMinimumPriceOfAllComponents()}");
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Get specific columns from all Components:");
        var items1 = _componentProviderSql.GetSpecificColumns();
        foreach (var item in items1)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Anonymous Class:");
        Console.WriteLine(_componentProviderSql.AnonymousClass());
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Name:");
        var items2 = _componentProviderSql.OrderByName();
        foreach (var item in items2)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Name, descending:");
        var items3 = _componentProviderSql.OrderByNameDescending();
        foreach (var item in items3)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Price and then by Name:");
        var items4 = _componentProviderSql.OrderByNameAndPrice();
        foreach (var item in items4)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(true);
    }
    public void ComponentProviderSqlTest()
    {
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Get Unique Names");
        var items = _componentProviderSql.GetUniqueNames();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Minimum Price");
        Console.WriteLine($"Minimum price of all Components {_componentProviderSql.GetMinimumPriceOfAllComponents()}");
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Get specific columns from all Components:");
        var items1 = _componentProviderSql.GetSpecificColumns();
        foreach (var item in items1)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Anonymous Class:");
        Console.WriteLine(_componentProviderSql.AnonymousClass());
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Name:");
        var items2 = _componentProviderSql.OrderByName();
        foreach (var item in items2)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(false);

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Name, descending:");
        var items3 = _componentProviderSql.OrderByNameDescending();
        foreach (var item in items3)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Components sorted by Price and then by Name:");
        var items4 = _componentProviderSql.OrderByNameAndPrice();
        foreach (var item in items4)
        {
            Console.WriteLine(item);
        }
        WaitTillKeyPressed(true);
    }
}