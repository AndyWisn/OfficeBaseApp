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
    public override string ToString() => String.Format("{0,-10}", "Customer") + base.ToString();
}