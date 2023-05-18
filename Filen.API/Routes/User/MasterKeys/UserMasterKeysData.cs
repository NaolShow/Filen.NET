using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received data from the <see cref="FilenAPI.UserMasterKeysRoute"/>
    /// </summary>
    public struct UserMasterKeysData {

        /// <inheritdoc cref="LoginData.EncryptedMasterKeys"/>
        [JsonPropertyName("keys")] public string EncryptedMasterKeys { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
