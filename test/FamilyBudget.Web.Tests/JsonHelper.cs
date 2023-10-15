using System.Text.Json;

namespace FamilyBudget.Web.Tests;

public static class JsonHelper
{
    public static T Deserialize<T>(this string json)
    {
        var options = new JsonSerializerOptions
        {
            IncludeFields = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        return JsonSerializer.Deserialize<T>(json, options)!;
    }
}


