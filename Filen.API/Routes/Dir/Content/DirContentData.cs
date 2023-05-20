using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received data from the <see cref="FilenAPI.DirContentRoute"/>
    /// </summary>
    public struct DirContentData {

        /// <summary>
        /// Represents the child <see cref="FolderData"/>
        /// </summary>
        [JsonPropertyName("folders")] public FolderData[] Folders { get; set; }
        /// <summary>
        /// Represents the child <see cref="FileData"/>
        /// </summary>
        [JsonPropertyName("uploads")] public FileData[] Files { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
