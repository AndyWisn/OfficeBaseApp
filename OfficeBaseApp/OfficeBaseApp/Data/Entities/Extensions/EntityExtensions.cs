using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using OfficeBaseApp.Data.Entities;
namespace OfficeBaseApp.Data.Entities.Extensions;
public static class EntityExtensions
{
    public static T? Copy<T>(this T itemToCopy) where T : class, IEntity
    {
        var json = JsonSerializer.Serialize(itemToCopy);
        return JsonSerializer.Deserialize<T>(json);
    }
}