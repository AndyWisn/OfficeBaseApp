namespace OfficeBaseApp.Entities;
public abstract class TradeGoodsBase : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public override string ToString() => string.Format("{0,-10} {1,-30} {2,-30}", $"|Id:{Id}", $"|Name: {Name}", $"|Info: {Description}");
}