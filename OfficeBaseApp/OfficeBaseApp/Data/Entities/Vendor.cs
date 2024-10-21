namespace OfficeBaseApp.Data.Entities;

public class Vendor : IEntity
{
    public Vendor()
    {
    }

    public Vendor(string name, string representativeFirstName, string representativeLastName, string contact, string certificate, string supportContact, string country)
    {
        Name = name;
        Contact = contact;
        VendorCertificate = certificate;
        Description = supportContact;
        Country = country;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Contact { get; set; }
    public string? VendorCertificate { get; set; }
    public string? Description { get; set; }
    public string? Country { get; set; }

    public void EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Vendor to repository:");
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        Name = Console.ReadLine();
        Console.WriteLine("Enter contact:");
        Contact = Console.ReadLine();
        Console.WriteLine("Enter certificates:");
        VendorCertificate = Console.ReadLine();
        Console.WriteLine("Enter support contact:");
        Description = Console.ReadLine();
        Console.WriteLine("Enter vendor's country:");
        Country = Console.ReadLine();
        Console.CursorVisible = false;
    }

    public override string ToString() => string.Format("{0,-5} {1,-10} {2,-10} {3,-20}", Id, Name, Country, Contact);
}