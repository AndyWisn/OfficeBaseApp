namespace OfficeBaseApp;
class Program
{
    static void Main(string[] args)
    {
        Console.Title = "OfficeBaseApp v1";
        InitRepositories.Init();
        TextMenu.MenuStart();
    }
}