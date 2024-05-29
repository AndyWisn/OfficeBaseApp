namespace OfficeBaseApp.Entities;
public class Component : TradeGoodsBase

{
    public Component()
    {
    }
    public Component(string name, float price, string descryption, int vendorId)
    {
        this.Name = name;
        this.Description = descryption;
        this.Price = price;
        this.ComponentVendorId = vendorId;

    }

    public Component(string name, string priceInString, string descryption, int vendorId)
    {
        this.Name = name;
        this.Description = descryption;
        this.ComponentVendorId = vendorId;

        if (float.TryParse(priceInString, out float result))
        {
            this.Price = result;
        }
        else
        {
            this.Price = float.NaN;
        }
    }

    public int ComponentVendorId { get; set; }
    public float Price { get; set; }

    public override string ToString() => string.Format("{0,-13} {1,-30} {2,-30}", "Component", base.ToString(), $"|VendoId: {ComponentVendorId}");
}