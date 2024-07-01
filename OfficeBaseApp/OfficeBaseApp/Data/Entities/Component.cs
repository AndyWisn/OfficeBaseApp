namespace OfficeBaseApp.Data.Entities;
public class Component : TradeGoodsBase
{
    public Component()
    {
    }
    public Component(string name, float price, string descryption, int vendorId)
    {
        Name = name;
        Description = descryption;
        Price = price;
        ComponentVendorId = vendorId;
    }
    public Component(string name, string priceInString, string descryption, int vendorId)
    {
        Name = name;
        Description = descryption;
        ComponentVendorId = vendorId;

        if (float.TryParse(priceInString, out float result))
        {
            Price = result;
        }
        else
        {
            Price = float.NaN;
        }
    }
    public override void EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Component to repository:");
        Console.WriteLine();
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        Name = Console.ReadLine();
        Console.WriteLine("Enter price:");
        string? priceInString = Console.ReadLine();
        if (float.TryParse(priceInString, out float result))
        {
            Price = result;
        }
        else
        {
            Price = float.NaN;
        }
        Console.WriteLine("Enter desciption:");
        Description = Console.ReadLine();
        Console.WriteLine("Enter vendor ID:");
        int.TryParse(Console.ReadLine(), out int vendorId);
        ComponentVendorId = vendorId;
        Console.CursorVisible = false;
    }
    public int ComponentVendorId { get; set; }
    public float Price { get; set; }
    public override string ToString() => string.Format("{0,-13} {1,-30} {2,-30}", "Component", base.ToString(), $"|VendoId: {ComponentVendorId}");
}