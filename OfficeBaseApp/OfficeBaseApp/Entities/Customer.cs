namespace OfficeBaseApp.Entities;

    public class Customer : BusinessPartnersBase
    {
        public Customer() 
        {
        }

        public Customer(string name)
        {
        }
        public override string ToString() => "Customer " + base.ToString();

    }