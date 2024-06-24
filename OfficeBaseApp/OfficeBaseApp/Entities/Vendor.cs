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
    public override void EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Vendor to repository:");
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        this.Name = Console.ReadLine();
        Console.WriteLine("Enter representative's name:");
        this.RepresentativeFirstName = Console.ReadLine();
        Console.WriteLine("Enter representative's surname:");
        this.RepresentativeLastName = Console.ReadLine();
        Console.WriteLine("Enter contact:");
        this.Contact = Console.ReadLine();
        Console.WriteLine("Enter certificates:");
        this.VendorCertificate = Console.ReadLine();
        Console.WriteLine("Enter support contact:");
        this.SupportContact = Console.ReadLine();
        Console.CursorVisible = false;
        //return new Vendor(name, repName, repSurname, contact, certificates, support);
    }
    public override string ToString() => string.Format("{0,-14}", "Vendor ") + base.ToString();
}