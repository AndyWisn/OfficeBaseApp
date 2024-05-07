namespace OfficeBaseApp.Entities
{
    public class Component : TradeGoodsBase
    {
        public Component()
        {
        }

        public Component(int id, string name, Vendor? vendor, float price)
        {
        }
        Vendor? Vendor { get; set; }
        float Price { get; set; }
        public override string ToString() => "Component " + this.ToString();

    }
}