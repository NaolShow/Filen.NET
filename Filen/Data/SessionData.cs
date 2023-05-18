using System.Text.Json.Serialization;

namespace Filen {

    /// <summary>
    /// Represents session data that is reused in <see cref="FilenClient.Login"/>
    /// </summary>
    public struct SessionData {

        /// <summary>
        /// Represents the user's API key
        /// </summary>
        [JsonPropertyName("apiKey")] public string ApiKey { get; set; }
        /// <summary>
        /// Represents the user's unencrypted master key
        /// </summary>
        [JsonPropertyName("masterKey")] public string MasterKey { get; set; }

    }

}
