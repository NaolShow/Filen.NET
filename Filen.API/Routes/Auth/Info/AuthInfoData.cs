using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received data from the <see cref="FilenAPI.AuthInfoRoute"/>
    /// </summary>
    public struct AuthInfoData {

        /// <summary>
        /// Represents the version of the user's authentication
        /// </summary>
        [JsonPropertyName("authVersion")] public int AuthenticationVersion { get; set; }
        /// <summary>
        /// Represents the user's email address
        /// </summary>
        [JsonPropertyName("email")] public string Email { get; set; }
        /// <summary>
        /// Represents the user's salt that will be used when derivating it's password
        /// </summary>
        [JsonPropertyName("salt")] public string Salt { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
