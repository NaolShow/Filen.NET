using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received data from the <see cref="FilenAPI.FileVersionsRoute"/>
    /// </summary>
    public struct FileVersionsData {

        /// <summary>
        /// Contains all the file versions that are available<br/>
        /// <br/>
        /// Please note that those instances will have their <see cref="FileData.Size"/> set to 0<br/>
        /// You might want to use <see cref="FileMetadata.Size"/> instead
        /// </summary>
        [JsonPropertyName("versions")] public FileData[] Versions { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
