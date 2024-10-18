namespace OfficeBaseApp.Data.Entities;
public class Product : IEntity
{
    public Product()
    {
    }
    public Product(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float Price { get; set; }

    public void EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Product to repository:");
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        Name = Console.ReadLine();
        Console.WriteLine("Enter descryption:");
        Description = Console.ReadLine();
        Console.CursorVisible = false;
    }
    public override string ToString() => string.Format("{0,-5} {1,-35} {2,-30} {3,-5}", Id, Name, Description, Price);
}