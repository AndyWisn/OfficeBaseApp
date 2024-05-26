using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;
using OfficeBaseApp.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;


namespace OfficeBaseApp;

class Program
{

    delegate void MenuItemAction();
    static Dictionary<(int, int), MenuItemAction> menuActionMap = new Dictionary<(int, int), MenuItemAction>();


    static List<List<string>> menu = new List<List<string>>()
    {
        new List<string> { "Initialize SQL Base with sample data", "Initialize the Json files with sample data", "List/Edit SQL repository", "Load/List/Edit Memory Repository", "Exit" },
        new List<string> { "Customers", "Vendors", "Components", "Products", "<-Back" },
        new List<string> { "Load", "Print", "Add", "Remove", "<-Back" }
    };
    static int actualItem = 0;
    static int menuLevel = 0;
    static List<int> menuPath = new List<int>();




    static void Main(string[] args)
    {



        Console.Title = "OfficeBaseApp v1";
        menuPath.Add(0);

        var customerListRepository = new ListRepository<Customer>();
        customerListRepository.SetUp();

        var vendorListRepository = new ListRepository<Vendor>();
        vendorListRepository.SetUp();

        var componentListRepository = new ListRepository<Component>();
        componentListRepository.SetUp();

        var productListRepository = new ListRepository<Product>();
        productListRepository.SetUp();


        void InitializeMenuActionMap()
        {
            menuActionMap[(0, 0)] = HandleMenuOption00;
            menuActionMap[(0, 1)] = () => HandleMenuOption01(customerListRepository, vendorListRepository, componentListRepository, productListRepository);
            menuActionMap[(0, 2)] = HandleMenuOptionGoDeeper;
            menuActionMap[(0, 3)] = HandleMenuOptionGoDeeper;
            menuActionMap[(0, 4)] = () => Environment.Exit(0);

            menuActionMap[(1, 0)] = HandleMenuOptionRepositorySelect;   // select repository menu
            menuActionMap[(1, 1)] = HandleMenuOptionRepositorySelect;   // select repository menu
            menuActionMap[(1, 2)] = HandleMenuOptionRepositorySelect;   // select repository menu
            menuActionMap[(1, 3)] = HandleMenuOptionRepositorySelect;   // select repository menu
            menuActionMap[(1, 4)] = HandleMenuOptionBack;

            menuActionMap[(2, 0)] = () =>
            {
                switch (menuPath[1])
                {
                    case 0:
                        LoadListRepository(customerListRepository);
                        break;
                    case 1:
                        LoadListRepository(vendorListRepository);
                        break;
                    case 2:
                        LoadListRepository(componentListRepository);
                        break;
                    case 3:
                        LoadListRepository(productListRepository);
                        break;
                }
            };
            menuActionMap[(2, 1)] = () =>
            {
                switch (menuPath[1])
                {
                    case 0:
                        PrintListRepository(customerListRepository);
                        break;
                    case 1:
                        PrintListRepository(vendorListRepository);
                        break;
                    case 2:
                        PrintListRepository(componentListRepository);
                        break;
                    case 3:
                        PrintListRepository(productListRepository);
                        break;
                }

            };
            menuActionMap[(2, 2)] = HandleMenuOption00;
            menuActionMap[(2, 3)] = HandleMenuOption00;
            menuActionMap[(2, 4)] = HandleMenuOptionBack;
        }


        InitializeMenuActionMap();

        MenuStart();







        Console.WriteLine("\nTest List repositories:");

        customerListRepository.LoadJson();
        vendorListRepository.LoadJson();
        componentListRepository.LoadJson();
        productListRepository.LoadJson();

        //AddCustomers(customerListRepository);
        //AddWholesaler(customerListRepository);
        customerListRepository.WriteAllToConsole();

        //AddVendor(vendorListRepository);
        vendorListRepository.WriteAllToConsole();

        //AddComponent(componentListRepository);
        componentListRepository.WriteAllToConsole();

        //AddProduct(productListRepository);
        productListRepository.WriteAllToConsole();



        var customerRepository = new SqlRepository<Customer>(new OfficeBaseAppDbContext());
        customerRepository.SetUp();

        var vendorRepository = new SqlRepository<Vendor>(new OfficeBaseAppDbContext());
        vendorRepository.SetUp();

        var componentRepository = new SqlRepository<Component>(new OfficeBaseAppDbContext());
        componentRepository.SetUp();

        var productRepository = new SqlRepository<Product>(new OfficeBaseAppDbContext());
        productRepository.SetUp();



        //static void CustomerAdded(object item)      //przerobic na generyk
        //{
        //    var customer = (Customer)item;
        //    Console.WriteLine($"{customer.Name} added");
        //}

        Console.WriteLine("\nTest EntityFramework Sql repositories:");
        //AddCustomers(customerRepository);
        //AddWholesaler(customerRepository);
        //customerRepository.Add(new Customer("Maxim (US)", "Greta", "Kruger", "+461276544125"));
        //customerRepository.WriteAllToConsole();
        //Console.WriteLine("---------------------------------------------------------------------------------------------");
        //customerRepository.Remove(customerRepository.GetItem("Maxim (US)"));
        //customerRepository.WriteAllToConsole();

        //vendorRepository.Add(new Vendor("Boeing INC", "Adam", "Carter", "+34568312973", "CE, FCC", "+423454563213"));
        //vendorRepository.WriteAllToConsole();
        //Console.WriteLine("---------------------------------------------------------------------------------------------");
        //vendorRepository.Remove(vendorRepository.GetItem("Boeing INC"));
        //vendorRepository.WriteAllToConsole();


    }





    static void MenuStart()
    {

        Console.CursorVisible = false;

        while (true)
        {
            PrintMenu();
            NavigateMenu();
            RunOption();
        }
    }

    static void PrintMenu()
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

    static void NavigateMenu()
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

    static void RunOption()
    {
        Console.Clear();
        //MenuItemAction runOption = menuActionMap[(menuLevel, ActualItem)];
        //runOption?.Invoke();
        menuActionMap[(menuLevel, actualItem)].Invoke();
    }

    static void HandleMenuOption00()                                        //Initialize SQL Base with sample data                
    {


        Console.SetCursorPosition(12, 1);
        Console.Write($"Opcja w budowie: {menuLevel},{actualItem}");
        Console.ReadKey();
    }


    //Initialize the Json files with sample data
    static void HandleMenuOption01(ListRepository<Customer> customerRepo, ListRepository<Vendor> vendorRepo, ListRepository<Component> componentRepo, ListRepository<Product> productRepo)                                        //Initialize Json files with sample data                
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==> Adding sample data to ListRepositories and save the Json files.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine();
        AddCustomers(customerRepo);
        AddVendors(vendorRepo);
        AddComponents(componentRepo);
        AddProducts(productRepo);
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
    }

    static void LoadListRepository<T>(ListRepository<T> repo) where T : class, IEntity, new()                                        //Load List repository                   
    {
        repo.LoadJson();
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
    }

    static void PrintListRepository<T>(ListRepository<T> Repo) where T : class, IEntity, new()                                         //Print List repository                
    {
        Repo.WriteAllToConsole();
        Console.WriteLine();
        Console.WriteLine("<Press any key to continue>");
        Console.ReadKey();
    }


    static void HandleMenuOptionRepositorySelect()                                         //Select repository repository                
    {
        //Console.WriteLine();
        menuLevel++;
        actualItem = 0;
        menuPath.Add(actualItem);

    }

    static void PrintPath()
    {
        (int, int) position = Console.GetCursorPosition();
        Console.SetCursorPosition(0, menu[menuLevel].Count+5);

        Console.Write($" Actual menu path:");
        foreach (var item in menuPath)
        {
            Console.Write(item);
        }
        Console.SetCursorPosition(position.Item1, position.Item2);
    }



    static void HandleMenuOptionBack()
    {
        menuLevel--;
        actualItem = menu[menuLevel].Count - 1;
        if (menuPath.Count > 0) { menuPath.RemoveAt(menuPath.Count - 1); }

    }

    static void HandleMenuOptionGoDeeper()
    {
        menuLevel++;
        actualItem = 0;
        menuPath.Add(actualItem);

    }

    static void HandleMenuOptionSelectRepo()
    {
        //repositoryTypeSelected = actualItem; 
        HandleMenuOptionGoDeeper();
    }


    static void AddCustomers(IRepository<Customer> customerRepo)
    {
        var customers = new[]
        {
                new Customer("AKG Industries A.G.", "John", "Walker", "+49123123123"),
                new Customer("Elemis Systems Inc.", "Ella", "McKenzie", "+43123123123"),
                new Customer("F-Ine Corp", "Ian", "Femming", "+44123123123"),
                new Customer("Microchip (UK)", "Barry", "Moore", "+44123123123")
            };
        customerRepo.AddBatch(customers);
    }
    static void AddVendors(IRepository<Vendor> vendorRepo)
    {
        Vendor[] vendors = new[]
       {
                new Vendor("Siemens A.G.", "John", "Ribena", "+4256453123123", "CE", "+421564563123"),
                new Vendor("Star Inc..", "Cris", "Cornell", "+454233123123", "CE, FCC", "+45654423123"),
                new Vendor("Molex", "Alan", "Wider", "+47532123123", "CE, RoHS", "+47133456423"),
                new Vendor("Mean-Well", "Ali", "Chang", "+47532123123", "CE, ,FCC, RoHS", "+4345656423")
            };
        vendorRepo.AddBatch(vendors);
    }
    static void AddWholesalers(IRepository<Customer> customerRepo)
    {
        Customer[] customers = new[]
        {
                new Wholesaler("ABB Corp.", "", "", "+482342634543"),
                new Wholesaler("Dellscape", "", "", "+481232594423"),
                new Wholesaler("Fine Inc.", "", "", "+481231563123"),
                new Wholesaler("Rutimex", "", "", "+481223431123")
            };
        customerRepo.AddBatch(customers);
    }
    static void AddComponents(IRepository<Component> componentsRepo)
    {
        var components = new[]
        {
            new Component("LM234", 14.45f, "Converter", 1),
            new Component("AXT435", 23.43f, "Connector", 2),
            new Component("IRM20-5", 123.40f, "AC/DC module", 3),
            new Component("CVB2423234", 0.21f, "Filter", 4)
            };
        componentsRepo.AddBatch(components);
    }
    static void AddProducts(IRepository<Product> productsRepo)
    {
        Product[] products = new[]
        {
            new Product("AKG1234PSU", "AKG hi-voltage PSU", new List<int>{ 1, 2, 3, 4 }),
            new Product("BKG2247PSU", "MLV lo-voltage PSU", new List<int>{ 1, 2, 2, 3 }),
            new Product("BKG2247PSU", "BSS mid-buffer PSU", new List<int>{ 1, 2, 3, 3 })
            };
        productsRepo.AddBatch(products);
    }
}