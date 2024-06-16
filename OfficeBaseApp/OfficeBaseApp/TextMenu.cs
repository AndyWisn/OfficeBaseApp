namespace OfficeBaseApp;
using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;
using OfficeBaseApp.Repositories.Extensions;

public class TextMenu : ITextMenu
{
    private delegate void MenuItemAction();
    private static Dictionary<(int, int), MenuItemAction> menuActionMap = new Dictionary<(int, int), MenuItemAction>();

    private static List<List<string>> menu = new List<List<string>>()
    {
        new List<string> { "Exit", "Create/restore SQL Base seeded sample data", "Seed sample data to Json files", "SQL repository", "Memory Repository"},
        new List<string> { "<-Back", "Customers", "Vendors", "Components", "Products"},
        new List<string> { "<-Back", "Load", "Print", "Add", "Remove"},
    };
    private static int actualItem = 0;
    private static int menuLevel = 0;
    private static List<int> menuPath = new List<int>();

    private readonly ListRepository<Customer> _customerListRepository;
    private readonly ListRepository<Vendor> _vendorListRepository;
    private readonly ListRepository<Component> _componentListRepository;
    private readonly ListRepository<Product> _productListRepository;

    private readonly SqlRepository<Customer> _customerSqlRepository;
    private readonly SqlRepository<Vendor> _vendorSqlRepository;
    private readonly SqlRepository<Component> _componentSqlRepository;
    private readonly SqlRepository<Product> _productSqlRepository;

    public TextMenu(ListRepository<Customer> customerListRepository,
                    ListRepository<Vendor> vendorListRepository,
                    ListRepository<Component> componentListRepository,
                    ListRepository<Product> productListRepository,
                    SqlRepository<Customer> customerSqlRepository,
                    SqlRepository<Vendor> vendorSqlRepository,
                    SqlRepository<Component> componentSqlRepository,
                    SqlRepository<Product> productSqlRepository)
    {
        _customerListRepository = customerListRepository;
        _vendorListRepository = vendorListRepository;
        _componentListRepository = componentListRepository;
        _productListRepository = productListRepository;
        _customerSqlRepository = customerSqlRepository;
        _vendorSqlRepository = vendorSqlRepository;
        _componentSqlRepository = componentSqlRepository;
        _productSqlRepository = productSqlRepository;
        _customerListRepository.SetUp();
        _vendorListRepository.SetUp();
        _componentListRepository.SetUp();
        _productListRepository.SetUp();
        _customerSqlRepository.SetUp();
        _vendorSqlRepository.SetUp();
        _componentSqlRepository.SetUp();
        _productSqlRepository.SetUp();
    }
    public void Run()
    {
        Console.CursorVisible = false;
        InitializeMenuActionMap();
        while (true)
        {
            PrintMenu();
            NavigateMenu();
            RunOption();
        }
    }
    public void InitializeMenuActionMap()
    {
        menuPath.Add(0);
        menuActionMap[(0, 0)] = () => Environment.Exit(0);
        menuActionMap[(0, 1)] = () => ResetDatabase(_customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository);
        menuActionMap[(0, 2)] = () => ResetJsonFiles(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository);
        menuActionMap[(0, 3)] = MenuGoDeeper;
        menuActionMap[(0, 4)] = MenuGoDeeper;

        menuActionMap[(1, 0)] = HandleMenuOptionBack;
        menuActionMap[(1, 1)] = MenuGoDeeper;
        menuActionMap[(1, 2)] = MenuGoDeeper;
        menuActionMap[(1, 3)] = MenuGoDeeper;
        menuActionMap[(1, 4)] = MenuGoDeeper;

        menuActionMap[(2, 0)] = HandleMenuOptionBack;
        menuActionMap[(2, 1)] = () =>                                                                 // Load repositories
            {
                if (menuPath[0] == 3)                                                                 //Load from SqlDataBase (check if base is connected, create if not).
                {
                    using (var context = new OfficeBaseAppDbContext())
                    {
                        if (context.Database.CanConnect())
                        {
                            Console.WriteLine("Database connected.");
                        }
                        else if (context.Database.EnsureCreated())
                        {
                            Console.WriteLine("Database created.");
                        }
                    }
                    WaitTillKeyPressed();
                }
                else if (menuPath[0] == 4)                                                              // Load repositories to memory (from JSON files).
                {
                    switch (menuPath[1])
                    {
                        case 1:
                            LoadListRepository(_customerListRepository);
                            break;
                        case 2:
                            LoadListRepository(_vendorListRepository);
                            break;
                        case 3:
                            LoadListRepository(_componentListRepository);
                            break;
                        case 4:
                            LoadListRepository(_productListRepository);
                            break;
                    }
                }
            };
        menuActionMap[(2, 2)] = () =>                                                           //Print Sql Repository, switch by repository type
        {
            if (menuPath[0] == 3)
            {
                switch (menuPath[1])
                {
                    case 1:
                        PrintRepository(_customerSqlRepository);
                        break;
                    case 2:
                        PrintRepository(_vendorSqlRepository);
                        break;
                    case 3:
                        PrintRepository(_componentSqlRepository);
                        break;
                    case 4:

                        PrintRepository(_productSqlRepository);
                        break;
                }
            }
            else if (menuPath[0] == 4)                                                          //Print List Repository, switch by repostory type
            {
                switch (menuPath[1])
                {
                    case 1:
                        PrintRepository(_customerListRepository);
                        break;
                    case 2:
                        PrintRepository(_vendorListRepository);
                        break;
                    case 3:
                        PrintRepository(_componentListRepository);
                        break;
                    case 4:
                        PrintRepository(_productListRepository);
                        break;
                }
            }
        };

        menuActionMap[(2, 3)] = () =>
        {
            if (menuPath[0] == 3)
            {
                switch (menuPath[1])
                {
                    case 1:
                        {
                            PrintRepository(_customerSqlRepository);
                            _customerSqlRepository.Add(new Customer().EnterPropertiesFromConsole());
                            PrintRepository(_customerSqlRepository);
                            break;
                        }
                    case 2:
                        {
                            PrintRepository(_vendorSqlRepository);
                            _vendorSqlRepository.Add(new Vendor().EnterPropertiesFromConsole());
                            PrintRepository(_vendorSqlRepository);
                            break;
                        }
                    case 3:
                        {
                            PrintRepository(_componentSqlRepository);
                            _componentSqlRepository.Add(new Component().EnterPropertiesFromConsole());
                            PrintRepository(_componentSqlRepository);
                            break;
                        }
                    case 4:
                        {
                            PrintRepository(_productSqlRepository);
                            _productSqlRepository.Add(new Product().EnterPropertiesFromConsole());
                            PrintRepository(_productSqlRepository);
                            break;
                        }
                }
            }
            else if (menuPath[0] == 4)
            {
                switch (menuPath[1])
                {
                    case 1:
                        {
                            PrintRepository(_customerListRepository);
                            _customerListRepository.Add(new Customer().EnterPropertiesFromConsole());
                            PrintRepository(_customerListRepository);
                            break;
                        }

                    case 2:
                        {
                            PrintRepository(_vendorListRepository);
                            _vendorListRepository.Add(new Vendor().EnterPropertiesFromConsole());
                            PrintRepository(_vendorListRepository);
                            break;
                        }
                    case 3:
                        {
                            PrintRepository(_componentListRepository);
                            _componentListRepository.Add(new Component().EnterPropertiesFromConsole());
                            PrintRepository(_componentListRepository);
                            break;
                        }
                    case 4:
                        {
                            PrintRepository(_productListRepository);
                            _productListRepository.Add(new Product().EnterPropertiesFromConsole());
                            PrintRepository(_productListRepository);
                            break;
                        }
                }
            }
        };
        menuActionMap[(2, 4)] = () =>                                                               // Remove Item from Repository
        {
            if (menuPath[0] == 3)
            {
                switch (menuPath[1])
                {
                    case 1:
                        {
                            RemoveItemFromRepository(_customerSqlRepository);
                            break;
                        }
                    case 2:
                        {
                            RemoveItemFromRepository(_vendorSqlRepository);
                            break;
                        }
                    case 3:
                        {
                            RemoveItemFromRepository(_componentSqlRepository);
                            break;
                        }
                    case 4:
                        {
                            RemoveItemFromRepository(_productSqlRepository);
                            break;
                        }
                }
            }
            else if (menuPath[0] == 4)
            {
                switch (menuPath[1])
                {
                    case 1:
                        {
                            RemoveItemFromRepository(_customerListRepository);
                            break;
                        }
                    case 2:
                        {
                            RemoveItemFromRepository(_vendorListRepository);
                            break;
                        }
                    case 3:
                        {
                            RemoveItemFromRepository(_componentListRepository);
                            break;
                        }
                    case 4:
                        {
                            RemoveItemFromRepository(_productListRepository);
                            break;
                        }
                }
            }
        };
    }
    public static void PrintMenu()
    {

        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("============= OfficeBaseApp v.1 Menu Options =============");
        Console.WriteLine("Move up/down with arrows. Confirm with Enter. Esc to exit.");
        Console.WriteLine();
        Console.WriteLine();
        for (int i = 0; i < menu[menuLevel].Count; i++)
        {
            if (i == actualItem)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine(menu[menuLevel][i]);
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public static void NavigateMenu()
    {
        do
        {
            PrintPath();
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if ((keyPressed.Key == ConsoleKey.UpArrow) ^ (keyPressed.Key == ConsoleKey.LeftArrow))
            {
                actualItem = (actualItem > 0) ? actualItem - 1 : menu[menuLevel].Count - 1;
                menuPath[menuLevel] = actualItem;
                PrintMenu();
            }
            else if ((keyPressed.Key == ConsoleKey.DownArrow) ^ (keyPressed.Key == ConsoleKey.RightArrow))
            {
                actualItem = (actualItem < menu[menuLevel].Count - 1) ? actualItem + 1 : 0;
                menuPath[menuLevel] = actualItem;
                PrintMenu();
            }
            else if (keyPressed.Key == ConsoleKey.Escape)
            {
                actualItem = 0;
                break;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                break;
            }
        }
        while (true);
    }
    public static void RunOption()
    {
        Console.Clear();
        menuActionMap[(menuLevel, actualItem)].Invoke();
    }
    public static void PrintPath()
    {
        (int, int) position = Console.GetCursorPosition();
        Console.SetCursorPosition(0, 2);
        Console.Write("Menu\\");
        //foreach (var item in menuPath)
        //{
        //    Console.Write(item);
        //}
        //Console.Write("->");
        for (var i = 0; i < menuPath.Count; i++)
        {
            Console.Write($"{menu[i][menuPath[i]]}\\");
        }
        Console.SetCursorPosition(position.Item1, position.Item2);
    }
    public static void ResetDatabase(SqlRepository<Customer> customerRepo, SqlRepository<Vendor> vendorRepo, SqlRepository<Component> componentRepo, SqlRepository<Product> productRepo)                                        //Initialize Json files with sample data                
    {
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
        WaitTillKeyPressed();
    }
    public static void ResetJsonFiles(ListRepository<Customer> customerRepo, ListRepository<Vendor> vendorRepo, ListRepository<Component> componentRepo, ListRepository<Product> productRepo)                                        //Initialize Json files with sample data                
    {
        Console.WriteLine();
        InitSampleData.AddCustomers(customerRepo);
        InitSampleData.AddVendors(vendorRepo);
        InitSampleData.AddComponents(componentRepo);
        InitSampleData.AddProducts(productRepo);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==> Sample data seeded to ListRepositories. New Json files saved.");
        Console.ForegroundColor = ConsoleColor.Gray;
        WaitTillKeyPressed();
    }
    public static void LoadListRepository<T>(ListRepository<T> repo) where T : class, IEntity, new()
    {
        repo.LoadJson();
        WaitTillKeyPressed();
    }
    public static void PrintRepository<T>(IRepository<T> repo) where T : class, IEntity, new()
    {
        Console.WriteLine();
        repo.WriteAllToConsole();
        WaitTillKeyPressed();
    }
    public static void RemoveItemFromRepository<T>(IRepository<T> repo) where T : class, IEntity, new()
    {
        PrintRepository(repo);
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
            }
            else
            {
                Console.WriteLine("Sorry, there's no item with such a name nor Id.");
            }
        }
        PrintRepository(repo);
    }
    public static void HandleMenuOptionBack()
    {
        if (menuLevel > 0) { menuLevel--; }
        actualItem = 0;
        if (menuPath.Count > 0) { menuPath.RemoveAt(menuPath.Count - 1); }
    }
    public static void MenuGoDeeper()
    {
        menuLevel++;
        actualItem = 1;
        menuPath.Add(actualItem);
    }
    public static void WaitTillKeyPressed()
    {
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
    }
}