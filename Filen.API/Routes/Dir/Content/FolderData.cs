using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received data from the <see cref="FilenAPI.DirContentRoute"/>
    /// </summary>
    public struct FolderData {

        /// <summary>
        /// Represents the unique ID of the folder
        /// </summary>
        [JsonPropertyName("uuid")] public string UUID { get; set; }
        /// <summary>
        /// Represents the encrypted <see cref="FolderMetadata"/> of the folder
        /// </summary>
        [JsonPropertyName("name")] public string EncryptedMetadata { get; set; }
        /// <summary>
        /// Represents the color of the folder (as plain text like 'blue', 'purple'...)
        /// </summary>
        [JsonPropertyName("color")] public string Color { get; set; }

        /// <summary>
        /// Determines if the folder is the default one or not<br/>
        /// (Utility not determined, I guess it means if it's the cloud drive or not, because we used to be able to have multiple cloud drives)
        /// </summary>
        [JsonPropertyName("is_default")] public int IsDefault { get; set; }
        /// <summary>
        /// Determines if the folder is synced or not<br/>
        /// (Utility not determined, after testing it's not equals to one if the folder has a public link)
        /// </summary>
        [JsonPropertyName("is_sync")] public int IsSync { get; set; }

        /// <summary>
        /// Determines the folder's parent UUID
        /// </summary>
        [JsonPropertyName("parent")] public string Parent { get; set; }
        /// <summary>
        /// Determines the time at which the folder has been uploaded<br/>
        /// (Use <see cref="DateTimeOffset.FromUnixTimeSeconds(long)"/> to get the actual date)
        /// </summary>
        [JsonPropertyName("timestamp")] public long Timestamp { get; set; }
        /// <summary>
        /// Determines if the folder is favorited or not
        /// </summary>
        [JsonPropertyName("favorited")] public int Favorited { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

    /// <summary>
    /// Represents the decrypted <see cref="FolderData.EncryptedMetadata"/>
    /// </summary>
    public struct FolderMetadata {

        /// <summary>
        /// Represents the name of the folder
        /// </summary>
        [JsonPropertyName("name")] public string Name { get; set; }

    }

}
