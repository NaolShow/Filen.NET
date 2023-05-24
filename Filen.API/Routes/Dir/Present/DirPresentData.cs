using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received data from the <see cref="FilenAPI.DirPresentRoute"/>
    /// </summary>
    public struct DirPresentData {

        /// <summary>
        /// Determines if the folder exists (even if this value is false, a file with the same <see cref="DirPresentRequest.UUID"/> can exist!)
        /// </summary>
        [JsonPropertyName("present")] public bool IsPresent { get; set; }
        /// <summary>
        /// Determines if the folder exists and is in the trash
        /// </summary>
        [JsonPropertyName("trash")] public bool IsTrashed { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
