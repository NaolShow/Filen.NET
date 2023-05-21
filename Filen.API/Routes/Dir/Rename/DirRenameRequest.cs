using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents sent data to the <see cref="FilenAPI.DirRenameRoute"/>
    /// </summary>
    public struct DirRenameRequest {

        /// <summary>
        /// Represents the folder's UUID
        /// </summary>
        [JsonPropertyName("uuid")] public string UUID { get; set; }

        /// <summary>
        /// Represents the folder's encrypted <see cref="FolderMetadata"/> containing it's new name
        /// </summary>
        [JsonPropertyName("name")] public string EncryptedMetadata { get; set; }
        /// <summary>
        /// Represents the folder's new hashed name encoded in hex (hashed lowercase name with SHA512 and result hashed with SHA1)
        /// </summary>
        [JsonPropertyName("nameHashed")] public string HashedName { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
