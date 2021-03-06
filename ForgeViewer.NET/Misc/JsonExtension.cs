using System;
using System.Text.Json;

namespace ForgeViewer.NET.Misc
{
    public static class JsonExtension
    {
        public static object? ToObject(this JsonElement? element, Type? type)
        {
            if (element is not { } jsonElement || type is not { })
                return null;
            
            var json = jsonElement.GetRawText();
            return JsonSerializer.Deserialize(json, type);
        }
    }
}