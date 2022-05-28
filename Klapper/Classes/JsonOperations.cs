using System.Text.Json;

namespace Klapper.Classes;

public class JsonOperations
{
    public static JsonElement GetJsonElement(JsonElement jsonElement, string path)
    {
        if (jsonElement.ValueKind == JsonValueKind.Null ||
            jsonElement.ValueKind == JsonValueKind.Undefined)
        {
            return default;
        }

        var segments =
            path.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var t in segments)
        {
            jsonElement = jsonElement.TryGetProperty(t, out JsonElement value) ? value : default;

            if (jsonElement.ValueKind == JsonValueKind.Null ||
                jsonElement.ValueKind == JsonValueKind.Undefined)
            {
                return default;
            }
        }

        return jsonElement;
    }
}