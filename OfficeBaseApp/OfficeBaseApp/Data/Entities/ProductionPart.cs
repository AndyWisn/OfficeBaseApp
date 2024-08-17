namespace OfficeBaseApp.Data.Entities;
public class ProductionPart : IEntity
{
    public ProductionPart()
    {
    }
    public ProductionPart(string name, float price, string description, string partVendor, string partManufacturer)
    {
        Name = name;
        Description = description;
        Price = price;
        PartManufacturer = partManufacturer;
        PartVendor = partVendor;    
    }
    public ProductionPart(string name, string priceInString, string descryption, string partVendor, string partManufacturer)
    {
        Name = name;
        Description = descryption;
        PartManufacturer = partManufacturer;
        PartVendor = partVendor;

        if (float.TryParse(priceInString, out float result))
        {
            Price = result;
        }
        else
        {
            Price = float.NaN;
        }
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PartManufacturer { get; set; }
    public string? Description { get; set; }
    public float Price { get; set; }
    public string? PartVendor { get; set; }

    public void EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Production Part to repository:");
        Console.WriteLine();
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        Name = Console.ReadLine();
        Console.WriteLine("Enter Manufacturer:");
        PartManufacturer = Console.ReadLine();
        float price;
        do
        {
            Console.WriteLine("Enter price:");
        }
        while (!float.TryParse(Console.ReadLine(), out price));
        Price = price;
        Console.WriteLine("Enter desciption:");
        Description = Console.ReadLine();
        Console.WriteLine("Enter vendor:");
        PartManufacturer = Console.ReadLine();
        Console.CursorVisible = false;
    }
    public override string ToString() => string.Format($"Part: {Name} Mfr:{PartManufacturer} Vendor:{PartVendor}");
}