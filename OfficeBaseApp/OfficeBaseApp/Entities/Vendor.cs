namespace OfficeBaseApp.Entities
{
    public class Vendor : BusinessPartnersBase
    {
        public Vendor()
        {
        }
        public Vendor(string name, string representativeFirstName, string representativeLastName, string contact, string certificate, string supportContact)
        {
            this.Name = name;
            this.RepresentativeFirstName = representativeFirstName;
            this.RepresentativeLastName = representativeLastName;
            this.Contact = contact;
            this.SupportContact = supportContact;
            this.VendorCertificate = certificate;
        }
        public string VendorCertificate { get; set; }
        public string SupportContact { get; set; }
        public override string ToString() => string.Format("{0,-14}", "Vendor ") + base.ToString();
    }
}