using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;
using OfficeBaseApp.Repositories.Extensions;
using OfficeBaseApp;

public class TextMenu
{

    delegate void MenuItemAction();
    static Dictionary<(int, int), MenuItemAction> menuActionMap = new Dictionary<(int, int), MenuItemAction>();
    static List<List<string>> Menu = new List<List<string>>()
{
new List<string> { "Initialize SQL Base with sample data", "Initialize the Json files with sample data", "List/Edit SQL repository", "List/Edit Memory Repository", "Exit" },
new List<string> { "Customers", "Vendors", "Components", "Products", "<-Back" },
new List<string> { "List", "Add", "Remove", "<-Back" }
};

    static int ActualItem = 0;
    static int MenuLevel = 0;

    //public static ListRepository<Customer> customerListRepository = new ListRepository<Customer>();
    

    //    var vendorListRepository = new ListRepository<Vendor>();
    //vendorListRepository.SetUp();

 //       var componentListRepository = new ListRepository<Component>();
 //   componentListRepository.SetUp();

   //     var productListRepository = new ListRepository<Product>();
    //productListRepository.SetUp();


    public static void MenuStart()
    {

        Console.CursorVisible = false;
        InitializeMenuActionMap();
        while (true)
        {
            PrintMenu();
            NavigateMenu();
            RunOption();
        }

        static void InitializeMenuActionMap()
        {
            menuActionMap[(0, 0)] = HandleMenuOption00;
            menuActionMap[(0, 1)] = HandleMenuOption00;
            menuActionMap[(0, 2)] = HandleMenuOptionGoDeeper;
            menuActionMap[(0, 3)] = HandleMenuOptionGoDeeper;
            menuActionMap[(0, 4)] = () => Environment.Exit(0);

            menuActionMap[(1, 0)] = HandleMenuOptionGoDeeper;
            menuActionMap[(1, 1)] = HandleMenuOptionGoDeeper;
            menuActionMap[(1, 2)] = HandleMenuOptionGoDeeper;
            menuActionMap[(1, 3)] = HandleMenuOptionGoDeeper;
            menuActionMap[(1, 4)] = HandleMenuOptionBack;

            menuActionMap[(2, 0)] = HandleMenuOption00;
            menuActionMap[(2, 1)] = HandleMenuOption00;
            menuActionMap[(2, 2)] = HandleMenuOption00;
            menuActionMap[(2, 3)] = HandleMenuOptionBack;
        }

        static void PrintMenu()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("============= OfficeBaseApp v.1 Menu Options =============");
            Console.WriteLine("Move up/down with arrows. Confirm with Enter. Esc to exit.");
            Console.WriteLine();

            for (int i = 0; i < Menu[MenuLevel].Count; i++)
            {
                if (i == ActualItem)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine(Menu[MenuLevel][i]);

            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void NavigateMenu()
        {

            do
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    ActualItem = (ActualItem > 0) ? ActualItem - 1 : Menu[MenuLevel].Count - 1;
                    PrintMenu();
                }
                else if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    ActualItem = (ActualItem < Menu[MenuLevel].Count - 1) ? ActualItem + 1 : 0;
                    PrintMenu();
                }
                else if (keyPressed.Key == ConsoleKey.Escape)
                {
                    ActualItem = Menu[MenuLevel].Count - 1;
                    break;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                    break;

            }
            while (true);
        }

        static void RunOption()
        {
            Console.Clear();
            MenuItemAction runOption = menuActionMap[(MenuLevel, ActualItem)];
            runOption?.Invoke();
        }

        static void HandleMenuOption00()                                        //Initialize SQL Base with sample data                
        {
            Console.SetCursorPosition(12, 1);
            Console.Write($"Opcja w budowie: {MenuLevel},{ActualItem}");
            Console.ReadKey();
        }

        void HandleMenuOption01()                                        //Initialize Json files with sample data                
        {
            AddCustomers(customerListRepository);
            Console.SetCursorPosition(12, 1);
            Console.Write($"Opcja w budowie: {MenuLevel},{ActualItem}");
            Console.ReadKey();
        }//Initialize the Json files with sample data

        static void HandleMenuOptionBack()
        {
            MenuLevel--;
            ActualItem = Menu[MenuLevel].Count - 1;
        }

        static void HandleMenuOptionGoDeeper()
        {
            MenuLevel++;
            ActualItem = 0;
        }





    }

}