namespace OfficeBaseApp.Entities;
public class Component : TradeGoodsBase

{
    public Component()
    {
    }
    public Component(string name, float price, string descryption, int vendorId)
    {
        this.Name = name;
        this.Description = descryption;
        this.Price = price;
        this.ComponentVendorId = vendorId;

    }

    public Component(string name, string priceInString, string descryption, int vendorId)
    {
        this.Name = name;
        this.Description = descryption;
        this.ComponentVendorId = vendorId;

        if (float.TryParse(priceInString, out float result))
        {
            this.Price = result;
        }
        else
        {
            this.Price = float.NaN;
        }
    }

    public Component EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Component to repository:");
        Console.WriteLine();
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        string? name = Console.ReadLine();
        Console.WriteLine("Enter price:");
        string? price = Console.ReadLine();
        Console.WriteLine("Enter desciption:");
        string? description = Console.ReadLine();
        Console.WriteLine("Enter vendor ID:");
        int.TryParse(Console.ReadLine(), out int vendorId);
        Console.CursorVisible = false;
        return new Component(name, price, description, vendorId);
    }

    public int ComponentVendorId { get; set; }
    public float Price { get; set; }

    public override string ToString() => string.Format("{0,-13} {1,-30} {2,-30}", "Component", base.ToString(), $"|VendoId: {ComponentVendorId}");
}