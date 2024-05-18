namespace OfficeBaseApp.Entities
{
    public class Component : TradeGoodsBase
 
    {
        public Component()
        {
        }
        public Component(string name, float price, string descryption)
        {
            this.Name = name;
            this.Description = descryption;
            this.Price = price; 
        }
        public Vendor ComponentsVendor { get; set; }
        public float Price { get; set; }

        public void AddVendor(Vendor vendor)
        { 
            this.ComponentsVendor = vendor;
        }

        public override string ToString() => string.Format("{0,-13} {1,-30} {2,-30}","Component", base.ToString(), $"|Vendor: {ComponentsVendor.Name}");
    }
}