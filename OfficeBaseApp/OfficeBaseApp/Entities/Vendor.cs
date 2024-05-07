namespace OfficeBaseApp.Entities
{
    public class Vendor : BusinessPartnerBase
    {
        public Vendor()
        {
        }
        public Vendor(string name)
        {
        }
        public override string ToString() => "Vendor " + this.ToString();
    }
}

