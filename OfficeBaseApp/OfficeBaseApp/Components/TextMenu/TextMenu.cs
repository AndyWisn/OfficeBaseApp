namespace OfficeBaseApp.Components.TextMenu;
public class TextMenu : ITextMenu
{
    public delegate void MenuItemAction();
    private List<MenuItemAction> _menuActionMap = new List<MenuItemAction>();
    private List<string> _menuItems = new List<string>();
    private int previousWidth = Console.WindowWidth;
    private int previousHeight = Console.WindowHeight;
    private ConsoleKeyInfo keyPressedOnExit;
    private bool _verticalArrowsActive = false;
    private bool _normalEnterAction = true;

    public TextMenu(List<string> menuItems, List<MenuItemAction> menuActionMap, bool verticalArrowsActive, bool normalEnterAction)
    {
        _menuItems = menuItems;
        _menuActionMap = menuActionMap;
        _verticalArrowsActive = verticalArrowsActive;
        _normalEnterAction = normalEnterAction;
    }
    public TextMenu(List<string> menuItems, List<MenuItemAction> menuActionMap)
    {
        _menuItems = menuItems;
        _menuActionMap = menuActionMap;
    }
    public ConsoleKeyInfo Run()
    {
        var actualItem = 1;
        Console.CursorVisible = false;
        bool menuIsActive = true;
       
        while (menuIsActive)
        {
            PrintMenu(actualItem);
            NavigateMenu(ref actualItem, ref menuIsActive);
            if (menuIsActive) {
                PrintHeader(); _menuActionMap[actualItem].Invoke();               
            }
        }
        PrintHeader();
        return keyPressedOnExit;
      
    }
    private void PrintMenu(int actualItem)
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
        Console.WriteLine();
        Console.WriteLine(); Console.WriteLine();
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
    private void NavigateMenu(ref int actualItem, ref bool menuIsActive)
    {
        do
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.UpArrow)
            {
                actualItem = actualItem > 0 ? actualItem - 1 : _menuItems.Count - 1;
                PrintMenu(actualItem);
            }
            else if (_verticalArrowsActive && (keyPressed.Key == ConsoleKey.LeftArrow))
            {
                menuIsActive = false;
                keyPressedOnExit = keyPressed;
                break;
            }
            else if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                actualItem = actualItem < _menuItems.Count - 1 ? actualItem + 1 : 0;
                PrintMenu(actualItem);
            }
            else if (_verticalArrowsActive && (keyPressed.Key == ConsoleKey.RightArrow))
            {
                menuIsActive = false;
                keyPressedOnExit = keyPressed;
                break;
            }
            else if (keyPressed.Key == ConsoleKey.Escape)
            {
                menuIsActive = false;
                actualItem = 0;
                keyPressedOnExit = keyPressed;
                break;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                if (_normalEnterAction) { menuIsActive = actualItem != 0; }
                else { PrintHeader();
                       _menuActionMap[actualItem].Invoke(); 
                       menuIsActive = false; }
                keyPressedOnExit = keyPressed;
                break;
            }
        }
        while (true);
    }
    public static void PrintHeader()
    {
        Console.Clear();
        Console.WriteLine("============= OfficeBaseApp v.1 Menu Options =============");
        Console.WriteLine("Move up/down with arrows. Confirm with Enter. Esc to exit.");
    }
}