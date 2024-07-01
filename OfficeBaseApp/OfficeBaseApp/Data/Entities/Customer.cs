namespace OfficeBaseApp.Data.Entities;
public class Customer : BusinessPartnersBase
{
    public Customer()
    {
    }
    public Customer(string name, string representativeFirstName, string representativeLastName, string contact)
    {
        Name = name;
        RepresentativeFirstName = representativeFirstName;
        RepresentativeLastName = representativeLastName;
        Contact = contact;
    }
    public override void EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Customer to repository:");
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        Name = Console.ReadLine();
        Console.WriteLine("Enter representative's name:");
        RepresentativeFirstName = Console.ReadLine();
        Console.WriteLine("Enter representative's surname:");
        RepresentativeLastName = Console.ReadLine();
        Console.WriteLine("Enter contact:");
        Contact = Console.ReadLine();
        Console.CursorVisible = false;
    }
    public override string ToString() => string.Format("{0,-14}", "Customer  ") + base.ToString();
}