using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents sent data to the <see cref="FilenAPI.DirPresentRoute"/>
    /// </summary>
    public struct DirPresentRequest {

        /// <summary>
        /// Represents the folder's UUID that might exist
        /// </summary>
        [JsonPropertyName("uuid")] public string UUID { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DirPresentRequest"/> containing the folder's UUID that might exist
        /// </summary>
        /// <param name="uuid">Folde's UUID that might exist</param>
        public DirPresentRequest(string uuid) => UUID = uuid;

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
