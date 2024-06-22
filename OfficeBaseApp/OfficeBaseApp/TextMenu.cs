namespace OfficeBaseApp;
using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;
using OfficeBaseApp.Repositories.Extensions;
using System;

public class TextMenu
{
    public delegate void MenuItemAction();
    private List<MenuItemAction> _menuActionMap = new List<MenuItemAction>();
    //    { () =>
    //        {
    //            Console.Clear();
    //            Environment.Exit(0);
    //        },
    //        () => ResetDatabase(_customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository),
    //        () => ResetJsonFiles(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository),
    //        () => {
    //                var subMenuItems= new List<string>  { "<-Back", "Customers", "Vendors", "Components", "Products"};
    //                var subMenuActions= new List<MenuItemAction>();
    //                var subMenu03 = new TextMenu(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository,
    //                                           _customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository, subMenuItems, subMenuActions);
    //                subMenu03.Run();
    //              }

    //};

    private List<string> _menuItems = new List<string>();
    //{"Exit", "Create/restore SQL Base seeded sample data", "Seed sample data to Json files", "SQL repository", "Memory Repository"};
    //{
    //        "Exit", "Create/restore SQL Base seeded sample data", "Seed sample data to Json files", "SQL repository", "Memory Repository"
    //new List<string> { "<-Back", "Customers", "Vendors", "Components", "Products"},
    //new List<string> { "<-Back", "Load", "Print", "Add", "Remove"},
    //};
        
    private static bool printHeader = true; //?
    
    private static int previousWidth = Console.WindowWidth;
    private static int previousHeight = Console.WindowHeight;

    private static List<int> menuPath = new List<int>();

    //private static ListRepository<Customer> _customerListRepository;
    //private static ListRepository<Vendor> _vendorListRepository;
    //private static ListRepository<Component> _componentListRepository;
    //private static ListRepository<Product> _productListRepository;

    //private static SqlRepository<Customer> _customerSqlRepository;
    //private static SqlRepository<Vendor> _vendorSqlRepository;
    //private static SqlRepository<Component> _componentSqlRepository;
    //private static SqlRepository<Product> _productSqlRepository;

    public TextMenu(
    //   ListRepository<Customer> customerListRepository,
    //                ListRepository<Vendor> vendorListRepository,
    //                ListRepository<Component> componentListRepository,
    //                ListRepository<Product> productListRepository,
    //                SqlRepository<Customer> customerSqlRepository,
    //                SqlRepository<Vendor> vendorSqlRepository,
    //                SqlRepository<Component> componentSqlRepository,
    //                SqlRepository<Product> productSqlRepository,
                     List<string> menuItems, List<MenuItemAction> menuActionMap)
    {
        //_customerListRepository = customerListRepository;
        //_vendorListRepository = vendorListRepository;
        //_componentListRepository = componentListRepository;
        //_productListRepository = productListRepository;
        //_customerSqlRepository = customerSqlRepository;
        //_vendorSqlRepository = vendorSqlRepository;
        //_componentSqlRepository = componentSqlRepository;
        //_productSqlRepository = productSqlRepository;
        //_customerListRepository.SetUp();
        //_vendorListRepository.SetUp();
        //_componentListRepository.SetUp();
        //_productListRepository.SetUp();
        //_customerSqlRepository.SetUp();
        //_vendorSqlRepository.SetUp();
        //_componentSqlRepository.SetUp();
        //_productSqlRepository.SetUp();
        _menuItems = menuItems;
        _menuActionMap = menuActionMap;
        //_menuActionMap[0]= () => { menuIsActive = false; }; 

    }
    public void Run()
    {
        var actualItem = 1;
        Console.CursorVisible = false;
        //InitializeMenuActionMap();
        printHeader = true;
        bool menuIsActive = true;
        while (menuIsActive)
        {
            PrintMenu(actualItem);
            NavigateMenu(ref actualItem, ref menuIsActive);
            (int, int) position = Console.GetCursorPosition();                   
            Console.SetCursorPosition(0,20);
            Console.WriteLine(actualItem);
            Console.SetCursorPosition(position.Item1, position.Item2);
            _menuActionMap[actualItem].Invoke();
        }
        Console.Clear();
        printHeader = true;
    }
    public void InitializeMenuActionMap()
    {
        //    menuPath.Add(0);
        //_menuActionMap[0] = () =>
        //{
        //Console.Clear();
        //Environment.Exit(0);
        //};
        //_menuActionMap[(1)] = () => ResetDatabase(_customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository);
        //_menuActionMap[(2)] = () => ResetJsonFiles(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository);
        //_menuActionMap[(3)] = () =>
        //  {
        //var subMenu = new TextMenu();
        //subMenu.Run();
        //};
        //_menuActionMap[(4)] = MenuGoDeeper;

        //    _menuActionMap[(1, 0)] = HandleMenuOptionBack;
        //    _menuActionMap[(1, 1)] = MenuGoDeeper;
        //    _menuActionMap[(1, 2)] = MenuGoDeeper;
        //    _menuActionMap[(1, 3)] = MenuGoDeeper;
        //    _menuActionMap[(1, 4)] = MenuGoDeeper;

        //    _menuActionMap[(2, 0)] = HandleMenuOptionBack;
        //    _menuActionMap[(2, 1)] = () =>                                                                 // Load repositories
        //        {
        //            if (menuPath[0] == 3)                                                                 //Load from SqlDataBase (check if base is connected, create if not).
        //            {
        //                using (var context = new OfficeBaseAppDbContext())
        //                {
        //                    Console.SetCursorPosition(0, menu[menuLevel].Count + 6);
        //                    if (context.Database.CanConnect())
        //                    {
        //                        Console.WriteLine("Database connected.");
        //                    }
        //                    else if (context.Database.EnsureCreated())
        //                    {
        //                        Console.WriteLine("Database created.");
        //                    }
        //                }
        //                WaitTillKeyPressed(true);
        //            }
        //            else if (menuPath[0] == 4)                                                              // Load repositories to memory (from JSON files).
        //            {
        //                switch (menuPath[1])
        //                {
        //                    case 1:
        //                        LoadListRepository(_customerListRepository);
        //                        break;
        //                    case 2:
        //                        LoadListRepository(_vendorListRepository);
        //                        break;
        //                    case 3:
        //                        LoadListRepository(_componentListRepository);
        //                        break;
        //                    case 4:
        //                        LoadListRepository(_productListRepository);
        //                        break;
        //                }
        //            }
        //        };
        //    _menuActionMap[(2, 2)] = () =>                                                           //Print Sql Repository, switch by repository type
        //    {
        //        if (menuPath[0] == 3)
        //        {
        //            switch (menuPath[1])
        //            {
        //                case 1:
        //                    PrintRepository(_customerSqlRepository);
        //                    WaitTillKeyPressed(true);
        //                    break;
        //                case 2:
        //                    PrintRepository(_vendorSqlRepository);
        //                    WaitTillKeyPressed(true);
        //                    break;
        //                case 3:
        //                    PrintRepository(_componentSqlRepository);
        //                    WaitTillKeyPressed(true);
        //                    break;
        //                case 4:
        //                    PrintRepository(_productSqlRepository);
        //                    WaitTillKeyPressed(true);
        //                    break;
        //            }
        //        }
        //        else if (menuPath[0] == 4)                                                          //Print List Repository, switch by repostory type
        //        {
        //            switch (menuPath[1])
        //            {
        //                case 1:
        //                    PrintRepository(_customerListRepository);
        //                    WaitTillKeyPressed(true);
        //                    break;
        //                case 2:
        //                    PrintRepository(_vendorListRepository);
        //                    WaitTillKeyPressed(true);
        //                    break;
        //                case 3:
        //                    PrintRepository(_componentListRepository);
        //                    WaitTillKeyPressed(true);
        //                    break;
        //                case 4:
        //                    PrintRepository(_productListRepository);
        //                    WaitTillKeyPressed(true);
        //                    break;
        //            }
        //        }
        //    };

        //    _menuActionMap[(2, 3)] = () =>
        //    {
        //        if (menuPath[0] == 3)
        //        {
        //            switch (menuPath[1])
        //            {
        //                case 1:
        //                    {
        //                        PrintRepository(_customerSqlRepository);
        //                        WaitTillKeyPressed(false);
        //                        _customerSqlRepository.Add(new Customer().EnterPropertiesFromConsole());
        //                        PrintRepository(_customerSqlRepository);
        //                        WaitTillKeyPressed(true);
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        PrintRepository(_vendorSqlRepository);
        //                        WaitTillKeyPressed(false);
        //                        _vendorSqlRepository.Add(new Vendor().EnterPropertiesFromConsole());
        //                        PrintRepository(_vendorSqlRepository);
        //                        WaitTillKeyPressed(true);
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        PrintRepository(_componentSqlRepository);
        //                        WaitTillKeyPressed(false);
        //                        _componentSqlRepository.Add(new Component().EnterPropertiesFromConsole());
        //                        PrintRepository(_componentSqlRepository);
        //                        WaitTillKeyPressed(true);
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        PrintRepository(_productSqlRepository);
        //                        WaitTillKeyPressed(false);
        //                        _productSqlRepository.Add(new Product().EnterPropertiesFromConsole());
        //                        PrintRepository(_productSqlRepository);
        //                        WaitTillKeyPressed(true);
        //                        break;
        //                    }
        //            }
        //        }
        //        else if (menuPath[0] == 4)
        //        {
        //            switch (menuPath[1])
        //            {
        //                case 1:
        //                    {
        //                        PrintRepository(_customerListRepository);
        //                        WaitTillKeyPressed(false);
        //                        _customerListRepository.Add(new Customer().EnterPropertiesFromConsole());
        //                        PrintRepository(_customerListRepository);
        //                        WaitTillKeyPressed(true);
        //                        break;
        //                    }

        //                case 2:
        //                    {
        //                        PrintRepository(_vendorListRepository);
        //                        WaitTillKeyPressed(false);
        //                        _vendorListRepository.Add(new Vendor().EnterPropertiesFromConsole());
        //                        PrintRepository(_vendorListRepository);
        //                        WaitTillKeyPressed(true);
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        PrintRepository(_componentListRepository);
        //                        WaitTillKeyPressed(false);
        //                        _componentListRepository.Add(new Component().EnterPropertiesFromConsole());
        //                        PrintRepository(_componentListRepository);
        //                        WaitTillKeyPressed(true);
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        PrintRepository(_productListRepository);
        //                        _productListRepository.Add(new Product().EnterPropertiesFromConsole());
        //                        PrintRepository(_productListRepository);
        //                        break;
        //                    }
        //            }
        //        }
        //    };
        //    _menuActionMap[(2, 4)] = () =>                                                               // Remove Item from Repository
        //    {
        //        if (menuPath[0] == 3)
        //        {
        //            switch (menuPath[1])
        //            {
        //                case 1:
        //                    {
        //                        RemoveItemFromRepository(_customerSqlRepository);
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        RemoveItemFromRepository(_vendorSqlRepository);
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        RemoveItemFromRepository(_componentSqlRepository);
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        RemoveItemFromRepository(_productSqlRepository);
        //                        break;
        //                    }
        //            }
        //        }
        //        else if (menuPath[0] == 4)
        //        {
        //            switch (menuPath[1])
        //            {
        //                case 1:
        //                    {
        //                        RemoveItemFromRepository(_customerListRepository);
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        RemoveItemFromRepository(_vendorListRepository);
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        RemoveItemFromRepository(_componentListRepository);
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        RemoveItemFromRepository(_productListRepository);
        //                        break;
        //                    }
        //            }
        //        }
        //    };
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
        (int, int) position1 = Console.GetCursorPosition();
        Console.SetCursorPosition(0, 20);
        Console.WriteLine(actualItem);
        Console.SetCursorPosition(position1.Item1, position1.Item2);
    }
    public void NavigateMenu(ref int actualItem, ref bool menuIsActive)
    {
        do
        {
            PrintPath();
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if ((keyPressed.Key == ConsoleKey.UpArrow) ^ (keyPressed.Key == ConsoleKey.LeftArrow))
            {
                actualItem = (actualItem > 0) ? actualItem - 1 : _menuItems.Count - 1;
                //menuPath[menuLevel] = actualItem;
                PrintMenu(actualItem);
            }
            else if ((keyPressed.Key == ConsoleKey.DownArrow) ^ (keyPressed.Key == ConsoleKey.RightArrow))
            {
                actualItem = (actualItem < _menuItems.Count - 1) ? actualItem + 1 : 0;
                //menuPath[menuLevel] = actualItem;
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
                if(actualItem==0)
                {
                    menuIsActive = false;
                }
                break;
            }
        }
        while (true);
    }
    

    public static void PrintPath()
    {

        Console.SetCursorPosition(0, 2);
        Console.Write("Menu\\");
        //foreach (var item in menuPath)
        //{
        //    Console.Write(item);
        //}
        //Console.Write("->");

        //for (var i = 0; i <= menuLevel; i++)
        //{
        //    Console.Write($"{_menuItems[menuPath[i]]}\\");
        //}
    }
    public static void ResetDatabase(SqlRepository<Customer> customerRepo, SqlRepository<Vendor> vendorRepo, SqlRepository<Component> componentRepo, SqlRepository<Product> productRepo, int position)                                        //Initialize Json files with sample data                
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
    public static void ResetJsonFiles(ListRepository<Customer> customerRepo, ListRepository<Vendor> vendorRepo, ListRepository<Component> componentRepo, ListRepository<Product> productRepo, int position)                                        //Initialize Json files with sample data                
    {
        Console.SetCursorPosition(0, position);
        Console.WriteLine();
        InitSampleData.AddCustomers(customerRepo);
        InitSampleData.AddVendors(vendorRepo);
        InitSampleData.AddComponents(componentRepo);
        InitSampleData.AddProducts(productRepo);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==> Sample data seeded to ListRepositories. New Json files saved.");
        Console.ForegroundColor = ConsoleColor.Gray;
        WaitTillKeyPressed(true);
    }
    public static void LoadListRepository<T>(ListRepository<T> repo, int position) where T : class, IEntity, new()
    {
        Console.SetCursorPosition(0, position);
        repo.LoadJson();
        WaitTillKeyPressed(true);
    }
    public static void PrintRepository<T>(IRepository<T> repo, int position) where T : class, IEntity, new()
    {
        Console.SetCursorPosition(0, position);
        repo.WriteAllToConsole();

    }
    public static void RemoveItemFromRepository<T>(IRepository<T> repo, int position) where T : class, IEntity, new()
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
                else {
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

    public static void WaitTillKeyPressed(bool clearConsole)
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

    public static void LoadSqlRepository(int position)
    {
        //Load from SqlDataBase (check if base is connected, create if not).

        using (var context = new OfficeBaseAppDbContext())
        {
            Console.SetCursorPosition(0, position);
            if (context.Database.CanConnect())
            {
                Console.WriteLine("Database connected.");
            }
            else if (context.Database.EnsureCreated())
            {
                Console.WriteLine("Database created.");
            }
        }
        WaitTillKeyPressed(true);
    
    }

    public static void PrintHeader()
    {
        Console.SetCursorPosition(0, 0);
        Console.Clear();
        Console.WriteLine("============= OfficeBaseApp v.1 Menu Options =============");
        Console.WriteLine("Move up/down with arrows. Confirm with Enter. Esc to exit.");
    }

}