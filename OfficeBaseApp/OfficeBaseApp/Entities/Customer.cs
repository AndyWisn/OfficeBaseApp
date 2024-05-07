namespace OfficeBaseApp.Entities
{
    public class Customer : BusinessPartnerBase
    {
        public Customer() 
        {
        }

        public Customer(string name)
        {
        }
        public override string ToString() => "Customer " + this.ToString();

    }
}