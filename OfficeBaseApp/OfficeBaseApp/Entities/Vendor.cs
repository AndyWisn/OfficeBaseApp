namespace OfficeBaseApp.Entities
{
    public class Vendor : BusinessPartnersBase
    {
        public Vendor()
        {
        }
        public Vendor(string name)
        {
        }
        public override string ToString() => "Vendor " + base.ToString();
    }
}

