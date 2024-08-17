using OfficeBaseApp.Data.Entities;

namespace OfficeBaseApp.Components.DataProviders;
public interface IProductionPartProvider : IDataProvider<ProductionPart>
{
    public float GetMinimumPriceOfAllParts();
    public List<ProductionPart> GetSpecificColumns();
    public string AnonymousClass();
    public List<ProductionPart> OrderByNameAndPrice();
}