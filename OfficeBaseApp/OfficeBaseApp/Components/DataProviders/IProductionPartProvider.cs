using OfficeBaseApp.Data.Entities;

namespace OfficeBaseApp.Components.DataProviders;
public interface IProductionPartProvider : IGenericDataProvider<ProductionPart>
{
    public float GetMinimumPriceOfAllParts();
    public List<ProductionPart> GetSpecificColumns();
    public string AnonymousClass();
    public List<ProductionPart> OrderByNameAndPrice();
}