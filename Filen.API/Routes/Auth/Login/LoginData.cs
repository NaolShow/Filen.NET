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
        /// Represents the user's encrypted master keys (as metadata), those keys are concatenated with the char '|' as separator<br/>
        /// There is one master key for each password that the account had
        /// </summary>
        [JsonPropertyName("masterKeys")] public string EncryptedMasterKeys { get; set; }
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
