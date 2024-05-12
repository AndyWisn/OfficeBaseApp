namespace OfficeBaseApp.Entities
{
    public class Component : TradeGoodsBase
    {
        public Component()
        {
        }

        public Component(int id, string name, Vendor? vendor, float price)
        {
            this.Name= name;
            this.Vendor= vendor;
            this.Price = price;
        }
        Vendor? Vendor { get; set; }
        public float Price { get; set; }
        public override string ToString() => "Component " + base.ToString();

    }
}