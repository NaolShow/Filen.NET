using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received data from the <see cref="FilenAPI.LoginRoute"/>
    /// </summary>
    public struct LoginData {

        /// <summary>
        /// Represents the account's API key that will be used to make further calls to the API
        /// </summary>
        [JsonPropertyName("apiKey")] public string ApiKey { get; set; }
        /// <summary>
        /// Represents the encrypted (as metadata) user's master keys (one for each password changed) used to decrypt metadata
        /// </summary>
        [JsonPropertyName("masterKeys")] public string MasterKeys { get; set; }
        /// <summary>
        /// Represents the user's public RSA key
        /// </summary>
        [JsonPropertyName("publicKey")] public string PublicKey { get; set; }
        /// <summary>
        /// Represents the encrypted (as metadata) user's private RSA key
        /// </summary>
        [JsonPropertyName("privateKey")] public string PrivateKey { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
