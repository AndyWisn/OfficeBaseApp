namespace OfficeBaseApp;
public class TextMenu
{
    public delegate void MenuItemAction();
    private List<MenuItemAction> _menuActionMap = new List<MenuItemAction>();
    private List<string> _menuItems = new List<string>();
    private int previousWidth = Console.WindowWidth;
    private int previousHeight = Console.WindowHeight;
    public TextMenu(List<string> menuItems, List<MenuItemAction> menuActionMap)
    {
        _menuItems = menuItems;
        _menuActionMap = menuActionMap;
    }
    public void Run()
    {
        var actualItem = 1;
        Console.CursorVisible = false;
        bool menuIsActive = true;
        while (menuIsActive)
        {
            PrintMenu(actualItem);
            NavigateMenu(ref actualItem, ref menuIsActive);
            _menuActionMap[actualItem].Invoke();
        }
        PrintHeader();
    }
    public void PrintMenu(int actualItem)
    {
        int currentWidth = Console.WindowWidth;
        int currentHeight = Console.WindowHeight;
        if (currentWidth != previousWidth || currentHeight != previousHeight)
        {
            previousWidth = currentWidth;
            previousHeight = currentHeight;
            PrintHeader();
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
    public static void PrintHeader()
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("============= OfficeBaseApp v.1 Menu Options =============");
        Console.WriteLine("Move up/down with arrows. Confirm with Enter. Esc to exit.");
    }
}