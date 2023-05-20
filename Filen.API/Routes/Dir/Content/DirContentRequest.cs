using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents sent data to the <see cref="FilenAPI.DirContentRoute"/>
    /// </summary>
    public struct DirContentRequest {

        /// <summary>
        /// Represents the UUID of the folder that will get it's content fetched
        /// </summary>
        [JsonPropertyName("uuid")] public string UUID { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DirContentRequest"/> containing the folder's UUID
        /// </summary>
        /// <param name="uuid">Folder's UUID that will get it's content fetched (for exemple the root folder acquired from <see cref="UserInfoData.BaseFolderUUID"/>)</param>
        public DirContentRequest(string uuid) => UUID = uuid;

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
