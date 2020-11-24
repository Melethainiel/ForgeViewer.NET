using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Misc
{
    public class JsonBoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.TokenType switch
            {
                JsonTokenType.String => throw new Exception("can't parse text"),
                JsonTokenType.Number => reader.GetInt32() == 1,
                JsonTokenType.True => true,
                JsonTokenType.False => false,
                JsonTokenType.Null => false,
                _ => false
            };

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value ? "1" : "0");
        }
    }
    
    public class JsonObjConverter : JsonConverter<object?>
    {
        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.TokenType switch
            {
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.Number => reader.GetDouble(),
                JsonTokenType.True => true,
                JsonTokenType.None => null,
                JsonTokenType.False => false,        
                JsonTokenType.Null => null,
                _ => throw new Exception($"can't parse {reader.TokenType}")
            };

        public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}