namespace OfficeBaseApp.Entities
{
    public class Component : TradeGoodsBase
    {
        public Component()
        {
        }
        public Component(string name, Vendor vendor, float price, string descryption)
        {
            this.Name = name;
            this.ComponentsVendor = vendor;
            this.Description = descryption;
            if (price == float.NaN) { this.Price = 0; }
            else { this.Price = price; }
        }
        public Vendor ComponentsVendor { get; set; }
        public float Price { get; set; }
        public override string ToString() => string.Format("{0,-13} {1,-30} {2,-30}","Component", base.ToString(), $"|Vendor: {ComponentsVendor.Name}");
    }
}