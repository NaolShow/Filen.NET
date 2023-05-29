using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents sent data to the <see cref="FilenAPI.DirTrashRoute"/>
    /// </summary>
    public struct DirTrashRequest {

        /// <summary>
        /// Represents the UUID of the folder that will get trashed
        /// </summary>
        [JsonPropertyName("uuid")] public string UUID { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DirTrashRequest"/> containing the folder's UUID that will get trashed
        /// </summary>
        /// <param name="uuid">Folder's UUID that will get trashed</param>
        public DirTrashRequest(string uuid) => UUID = uuid;

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
