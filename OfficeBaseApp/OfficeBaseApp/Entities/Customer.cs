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
    public override string ToString() => string.Format("{0,-14}", "Customer  ") + base.ToString();
}