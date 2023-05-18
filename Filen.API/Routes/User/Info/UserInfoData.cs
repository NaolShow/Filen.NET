using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received data from the <see cref="FilenAPI.UserInfoRoute"/>
    /// </summary>
    public struct UserInfoData {

        /// <summary>
        /// Represents the user's root folder UUID
        /// </summary>
        [JsonPropertyName("baseFolderUUID")] public string BaseFolderUUID { get; set; }

        /// <summary>
        /// Represents a URL that points towards the user's avatar (not encrypted can straight read/display it)
        /// </summary>
        [JsonPropertyName("avatarURL")] public string AvatarUrl { get; set; }
        /// <summary>
        /// Represents the user's unique ID
        /// </summary>
        [JsonPropertyName("id")] public int ID { get; set; }
        /// <summary>
        /// Represents the user's email address
        /// </summary>
        [JsonPropertyName("email")] public string Email { get; set; }
        /// <summary>
        /// Determines if the user is premium or not (has already bought any plan or is a free account)<br/>
        /// (1 for premium, 0 for free account)
        /// </summary>
        [JsonPropertyName("isPremium")] public int Premium { get; set; }

        /// <summary>
        /// Represents the maximum available space in the user's storage (in bytes) 
        /// </summary>
        [JsonPropertyName("maxStorage")] public long MaxStorage { get; set; }
        /// <summary>
        /// Represents the used space in the user's storage (in bytes)
        /// </summary>
        [JsonPropertyName("storageUsed")] public long UsedStorage { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
