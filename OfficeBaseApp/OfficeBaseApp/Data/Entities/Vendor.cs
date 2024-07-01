namespace OfficeBaseApp.Data.Entities;
public class Vendor : BusinessPartnersBase
{
    public Vendor()
    {
    }
    public Vendor(string name, string representativeFirstName, string representativeLastName, string contact, string certificate, string supportContact)
    {
        Name = name;
        RepresentativeFirstName = representativeFirstName;
        RepresentativeLastName = representativeLastName;
        Contact = contact;
        VendorCertificate = certificate;
        SupportContact = supportContact;
    }
    public string VendorCertificate { get; set; }
    public string SupportContact { get; set; }
    public override void EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Vendor to repository:");
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        Name = Console.ReadLine();
        Console.WriteLine("Enter representative's name:");
        RepresentativeFirstName = Console.ReadLine();
        Console.WriteLine("Enter representative's surname:");
        RepresentativeLastName = Console.ReadLine();
        Console.WriteLine("Enter contact:");
        Contact = Console.ReadLine();
        Console.WriteLine("Enter certificates:");
        VendorCertificate = Console.ReadLine();
        Console.WriteLine("Enter support contact:");
        SupportContact = Console.ReadLine();
        Console.CursorVisible = false;
        //return new Vendor(name, repName, repSurname, contact, certificates, support);
    }
    public override string ToString() => string.Format("{0,-14}", "Vendor ") + base.ToString();
}