using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Converts enumerations to their lowercase string equivalent during serialization and get them back during deserialization
    /// </summary>
    /// <typeparam name="TEnum">The enumeration that was be serialized/deserialized</typeparam>
    public class JsonLowerCaseEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum {

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Enum.Parse<TEnum>(reader.GetString(), ignoreCase: true);
        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString().ToLower());

    }

}
