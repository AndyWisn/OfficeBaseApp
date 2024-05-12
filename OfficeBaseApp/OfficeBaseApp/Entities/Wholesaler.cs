namespace OfficeBaseApp.Entities;

public class Wholesaler : Customer
{
    public Wholesaler()
    {
    }

    public Wholesaler(string name, string representativeFirstName, string representativeLastName, string contact)
    {
        this.Name = name;
        this.RepresentativeFirstName = representativeFirstName;
        this.RepresentativeLastName = representativeLastName;
        this.Contact = contact;
    }
    
    public override string ToString() => "Wholesale" + base.ToString();

}