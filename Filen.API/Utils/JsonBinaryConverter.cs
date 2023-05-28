using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Converts numbers that ranges from 0 to 1 into boolean equivalents (with 0 = false and 1 = true)
    /// Converts enumerations to their lowercase string equivalent during serialization and get them back during deserialization
    /// </summary>
    public class JsonBinaryConverter : JsonConverter<bool> {

        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType == JsonTokenType.Number && reader.GetInt32() == 1;
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteNumberValue(value ? 1 : 0);

    }

}
