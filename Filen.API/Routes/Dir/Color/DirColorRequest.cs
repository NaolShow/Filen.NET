using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents sent data to the <see cref="FilenAPI.DirColorRoute"/>
    /// </summary>
    public struct DirColorRequest {

        /// <summary>
        /// Represents the UUID of the folder that will get its color changed
        /// </summary>
        [JsonPropertyName("uuid")] public string UUID { get; set; }
        /// <summary>
        /// Represents the new color of the folder
        /// </summary>
        [JsonPropertyName("color")] public DirColor Color { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DirColorRequest"/> containing the folder's UUID and it's new color
        /// </summary>
        /// <param name="uuid">Folder's UUID that will get it's color changed</param>
        /// <param name="color">New folder's color</param>
        public DirColorRequest(string uuid, DirColor color) {
            UUID = uuid;
            Color = color;
        }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
