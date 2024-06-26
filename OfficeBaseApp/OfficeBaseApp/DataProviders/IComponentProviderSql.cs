using OfficeBaseApp.Entities;
namespace OfficeBaseApp.DataProviders;
public interface IComponentProviderSql : ICommonDataProvider<Component>
{
    public float GetMinimumPriceOfAllComponents();
    public List<Component> GetSpecificColumns();
    public string AnonymousClass();
    public List<Component> OrderByNameAndPrice();
}