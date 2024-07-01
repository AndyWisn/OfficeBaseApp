using OfficeBaseApp.Data.Entities;

namespace OfficeBaseApp.Components.DataProviders;
public interface IComponentProvider : IDataProvider<Component>
{
    public float GetMinimumPriceOfAllComponents();
    public List<Component> GetSpecificColumns();
    public string AnonymousClass();
    public List<Component> OrderByNameAndPrice();
}