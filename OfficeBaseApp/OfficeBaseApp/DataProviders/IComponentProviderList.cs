using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
namespace OfficeBaseApp.DataProviders;

public interface IComponentProviderList : ICommonDataProvider<Component>
{
    public float GetMinimumPriceOfAllComponents();

    public List<Component> GetSpecificColumns();

    public string AnonymousClass();
    public List<Component> OrderByNameAndPrice();

}
