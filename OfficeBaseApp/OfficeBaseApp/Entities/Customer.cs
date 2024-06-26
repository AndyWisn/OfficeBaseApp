namespace OfficeBaseApp.Entities;
public class Customer : BusinessPartnersBase
{
    public Customer()
    {
    }
    public Customer(string name, string representativeFirstName, string representativeLastName, string contact)
    {
        this.Name = name;
        this.RepresentativeFirstName = representativeFirstName;
        this.RepresentativeLastName = representativeLastName;
        this.Contact = contact;
    }
    public override void EnterPropertiesFromConsole()
    { 
        Console.WriteLine();
        Console.WriteLine($"Add new Customer to repository:");
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        this.Name = Console.ReadLine();
        Console.WriteLine("Enter representative's name:");
        this.RepresentativeFirstName = Console.ReadLine();
        Console.WriteLine("Enter representative's surname:");
        this.RepresentativeLastName = Console.ReadLine();
        Console.WriteLine("Enter contact:");
        this.Contact = Console.ReadLine();
        Console.CursorVisible = false;
        //return new Customer(name, repName, repSurname, contact);
    }

    public override string ToString() => string.Format("{0,-14}", "Customer  ") + base.ToString();
}