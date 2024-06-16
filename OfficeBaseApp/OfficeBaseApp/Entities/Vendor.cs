namespace OfficeBaseApp.Entities;

public class Vendor : BusinessPartnersBase
{
    public Vendor()
    {
    }
    public Vendor(string name, string representativeFirstName, string representativeLastName, string contact, string certificate, string supportContact)
    {
        this.Name = name;
        this.RepresentativeFirstName = representativeFirstName;
        this.RepresentativeLastName = representativeLastName;
        this.Contact = contact;
        this.VendorCertificate = certificate;
        this.SupportContact = supportContact;
    }
    public string VendorCertificate { get; set; }
    public string SupportContact { get; set; }

    public Vendor EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Vendor to repository:");
        Console.WriteLine();
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        string? name = Console.ReadLine();
        Console.WriteLine("Enter representative's name:");
        string? repName = Console.ReadLine();
        Console.WriteLine("Enter representative's surname:");
        string? repSurname = Console.ReadLine();
        Console.WriteLine("Enter contact:");
        string? contact = Console.ReadLine();
        Console.WriteLine("Enter certificates:");
        string? certificates = Console.ReadLine();
        Console.WriteLine("Enter support contact:");
        string? support = Console.ReadLine();
        Console.CursorVisible = false;
        return new Vendor(name, repName, repSurname, contact, certificates, support);

    }


    public override string ToString() => string.Format("{0,-14}", "Vendor ") + base.ToString();
}