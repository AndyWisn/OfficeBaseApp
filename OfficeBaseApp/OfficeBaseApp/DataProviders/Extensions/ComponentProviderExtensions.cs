using OfficeBaseApp.Entities;
namespace OfficeBaseApp.DataProviders.Extensions;
public static class DataProvidernentsHelper
{
    public static IEnumerable<Component> ByDescription(this IEnumerable<Component> query, string descritption)
    {
        return query.Where(x => x.Description == descritption);
    }
}