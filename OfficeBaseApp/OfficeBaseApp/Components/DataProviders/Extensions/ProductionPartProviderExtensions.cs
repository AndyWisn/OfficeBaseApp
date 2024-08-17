using OfficeBaseApp.Data.Entities;

namespace OfficeBaseApp.Components.DataProviders.Extensions;
public static class DataProvidernentsHelper
{
    public static IEnumerable<ProductionPart> ByDescription(this IEnumerable<ProductionPart> query, string descritption)
    {
        return query.Where(x => x.Description == descritption);
    }
}