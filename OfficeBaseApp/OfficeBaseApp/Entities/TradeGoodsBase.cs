namespace OfficeBaseApp.Entities;
public abstract class TradeGoodsBase : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }


}