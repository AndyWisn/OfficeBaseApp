﻿using System.Text.Json;
namespace OfficeBaseApp.Data.Entities.Extensions;

public static class EntityExtensions
{
    public static T? Copy<T>(this T itemToCopy) where T : class, IEntity
    {
        var json = JsonSerializer.Serialize(itemToCopy);
        return JsonSerializer.Deserialize<T>(json);
    }
}