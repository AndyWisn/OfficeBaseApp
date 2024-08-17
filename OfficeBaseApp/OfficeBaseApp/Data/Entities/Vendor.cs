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
        SupportContact = supportContact;
        Country = country;
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Contact { get; set; }
    public string? VendorCertificate { get; set; }
    public string? SupportContact { get; set; }
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
        SupportContact = Console.ReadLine();
        Console.WriteLine("Enter vendor's country:");
        Country = Console.ReadLine();
        Console.CursorVisible = false;
        //return new Vendor(name, repName, repSurname, contact, certificates, support);
    }
    public override string ToString() => string.Format($"Vendor:{Name} Country:{Country}");
}