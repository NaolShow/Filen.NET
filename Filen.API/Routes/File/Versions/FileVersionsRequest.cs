using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents sent data to the <see cref="FilenAPI.FileVersionsRoute"/>
    /// </summary>
    public struct FileVersionsRequest {

        /// <summary>
        /// Represents the UUID of the file that is going to have its versions fetched
        /// </summary>
        [JsonPropertyName("uuid")] public string UUID { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="FileVersionsRequest"/> containing the file UUID that will get fetched
        /// </summary>
        /// <param name="uuid">File UUID that will get fetched</param>
        public FileVersionsRequest(string uuid) => UUID = uuid;

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
