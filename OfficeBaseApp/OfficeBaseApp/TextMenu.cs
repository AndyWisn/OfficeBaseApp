namespace OfficeBaseApp;

using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;
using OfficeBaseApp.Repositories.Extensions;
using OfficeBaseApp;
using System.ComponentModel.Design;

public static class TextMenu
{
    delegate void MenuItemAction();
    static Dictionary<(int, int), MenuItemAction> menuActionMap = new Dictionary<(int, int), MenuItemAction>();


    static List<List<string>> menu = new List<List<string>>()
    {
        new List<string> { "Create/restore SQL Base seeded with sample data", "Seed sample data to Json files", "List/Edit SQL repository", "Load/List/Edit Memory Repository", "Exit" },
        new List<string> { "Customers", "Vendors", "Components", "Products", "<-Back" },
        new List<string> { "Load", "Print", "Add", "Remove", "<-Back" }
    };
    static int actualItem = 0;
    static int menuLevel = 0;
    static List<int> menuPath = new List<int>();


    public static void MenuStart()
    {

        Console.CursorVisible = false;


        while (true)
        {
            PrintMenu();
            NavigateMenu();
            RunOption();
        }
    }

    public static void InitializeMenuActionMap()
    {
        menuPath.Add(0);
        menuActionMap[(0, 0)] = () => HandleMenuOption00(InitRepositories.customerRepository,
                                                         InitRepositories.vendorRepository,
                                                         InitRepositories.componentRepository,
                                                         InitRepositories.productRepository
                                                         );
        menuActionMap[(0, 1)] = () => HandleMenuOption01(InitRepositories.customerListRepository,
                                                         InitRepositories.vendorListRepository,
                                                         InitRepositories.componentListRepository,
                                                         InitRepositories.productListRepository
                                                         );
        menuActionMap[(0, 2)] = HandleMenuOptionGoDeeper;
        menuActionMap[(0, 3)] = HandleMenuOptionGoDeeper;
        menuActionMap[(0, 4)] = () => Environment.Exit(0);

        menuActionMap[(1, 0)] = RepositorySelectMenu;                                                 // select repository type menu
        menuActionMap[(1, 1)] = RepositorySelectMenu;                                                 // select repository type menu
        menuActionMap[(1, 2)] = RepositorySelectMenu;                                                 // select repository type menu
        menuActionMap[(1, 3)] = RepositorySelectMenu;                                                // select repository type menu
        menuActionMap[(1, 4)] = HandleMenuOptionBack;


        menuActionMap[(2, 0)] = () =>                                                                 // Load from SqlDataBase (check if base is connected, create if not).
            {
                if (menuPath[0] == 2)
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
                    Console.WriteLine();
                    Console.WriteLine("<Press any key to continue>");
                    Console.ReadKey();
                }
                else if (menuPath[0] == 3)
                {
                    switch (menuPath[1])
                    {
                        case 0:
                            LoadListRepository(InitRepositories.customerListRepository);
                            break;
                        case 1:
                            LoadListRepository(InitRepositories.vendorListRepository);
                            break;
                        case 2:
                            LoadListRepository(InitRepositories.componentListRepository);
                            break;
                        case 3:
                            LoadListRepository(InitRepositories.productListRepository);
                            break;
                    }
                }
            };
        menuActionMap[(2, 1)] = () =>                                                           //Print Sql Repository, switch by repostory type
        {
            if (menuPath[0] == 2)
            {
                switch (menuPath[1])
                {
                    case 0:
                        PrintRepository(InitRepositories.customerRepository);
                        break;
                    case 1:
                        PrintRepository(InitRepositories.vendorRepository);
                        break;
                    case 2:
                        PrintRepository(InitRepositories.componentRepository);
                        break;
                    case 3:

                        PrintRepository(InitRepositories.productRepository);
                        break;
                }
            }
            else if (menuPath[0] == 3)                                                          //Print List Repository, switch by repostory type
            {
                switch (menuPath[1])
                {
                    case 0:
                        PrintRepository(InitRepositories.customerListRepository);
                        break;
                    case 1:
                        PrintRepository(InitRepositories.vendorListRepository);
                        break;
                    case 2:
                        PrintRepository(InitRepositories.componentListRepository);
                        break;
                    case 3:
                        PrintRepository(InitRepositories.productListRepository);
                        break;
                }
            }
        };

        menuActionMap[(2, 3)] = () =>                                                               // Remove Item from Repository
        {
            if (menuPath[0] == 2)
            {
                switch (menuPath[1])
                {
                    case 0:
                        {
                            RemoveItemFromRepository(InitRepositories.customerRepository);
                            break;
                        }
                    case 1:
                        {
                            RemoveItemFromRepository(InitRepositories.vendorRepository);
                            break;
                        }
                    case 2:
                        {
                            RemoveItemFromRepository(InitRepositories.componentRepository);
                            break;
                        }
                    case 3:
                        {
                            RemoveItemFromRepository(InitRepositories.productRepository);
                            break;
                        }
                }
            }
            else if (menuPath[0] == 3)
            {
                switch (menuPath[1])
                {
                    case 0:
                        {
                            RemoveItemFromRepository(InitRepositories.customerListRepository);
                            break;
                        }

                    case 1:
                        {
                            RemoveItemFromRepository(InitRepositories.vendorListRepository);
                            break;
                        }
                    case 2:
                        {
                            RemoveItemFromRepository(InitRepositories.componentListRepository);
                            break;
                        }
                    case 3:
                        {
                            RemoveItemFromRepository(InitRepositories.productListRepository);
                            break;
                        }
                }
            }
        };

        menuActionMap[(2, 2)] = HandleMenuOptionDoNothing;                                              //Add item to repository option

        menuActionMap[(2, 4)] = HandleMenuOptionBack;                                                   // Back Menu Option
    }
    public static void PrintMenu()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("============= OfficeBaseApp v.1 Menu Options =============");
        Console.WriteLine("Move up/down with arrows. Confirm with Enter. Esc to exit.");
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
                if (menuLevel >= 0) { menuLevel--; }

                actualItem = menu[menuLevel].Count - 1;
                if (menuPath.Count > 0) { menuPath.RemoveAt(menuPath.Count - 1); }
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
        Console.SetCursorPosition(0, menu[menuLevel].Count + 5);

        Console.Write($" Actual menu path:");
        foreach (var item in menuPath)
        {
            Console.Write(item);
        }
        Console.SetCursorPosition(position.Item1, position.Item2);
    }
    public static void HandleMenuOptionDoNothing()
    { }
    public static void HandleMenuOption00(SqlRepository<Customer> customerRepo, SqlRepository<Vendor> vendorRepo, SqlRepository<Component> componentRepo, SqlRepository<Product> productRepo)                                        //Initialize Json files with sample data                
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
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
    }
    public static void HandleMenuOption01(ListRepository<Customer> customerRepo, ListRepository<Vendor> vendorRepo, ListRepository<Component> componentRepo, ListRepository<Product> productRepo)                                        //Initialize Json files with sample data                
    {
        Console.WriteLine();
        InitSampleData.AddCustomers(customerRepo);
        InitSampleData.AddVendors(vendorRepo);
        InitSampleData.AddComponents(componentRepo);
        InitSampleData.AddProducts(productRepo);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==> Sample data seeded to ListRepositories. New Json files saved.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
    }
    public static void LoadListRepository<T>(ListRepository<T> repo) where T : class, IEntity, new()                                        //Load List repository                   
    {
        repo.LoadJson();
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
    }
    public static void PrintRepository<T>(IRepository<T> Repo) where T : class, IEntity, new()                                               //Print List repository                
    {
        Repo.WriteAllToConsole();
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
    }
    public static void RepositorySelectMenu()                                                                                                //Select repository repository                
    {
        //Console.WriteLine();
        menuLevel++;
        actualItem = 0;
        menuPath.Add(actualItem);
    }
    public static void RemoveItemFromRepository<T>(IRepository<T> repo) where T : class, IEntity, new()
    {
        PrintRepository(repo);
        Console.WriteLine();
        Console.WriteLine("Enter the item's name or Id number: ");
        var itemName = Console.ReadLine();
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
                Console.WriteLine("Sorry no item with such name nor Id.");
            }
        }
        PrintRepository(repo);
    }
    public static void HandleMenuOptionBack()
    {
        menuLevel--;
        actualItem = menu[menuLevel].Count - 1;
        if (menuPath.Count > 0) { menuPath.RemoveAt(menuPath.Count - 1); }
    }
    public static void HandleMenuOptionGoDeeper()
    {
        menuLevel++;
        actualItem = 0;
        menuPath.Add(actualItem);
    }
}