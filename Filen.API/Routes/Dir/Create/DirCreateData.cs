using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received data from the <see cref="FilenAPI.DirCreateRoute"/>
    /// </summary>
    public struct DirCreateData {

        /// <summary>
        /// Represents the folder's UUID that has been passed in the <see cref="DirCreateRequest.UUID"/><br/>
        /// If a folder in the same place and with the same name already exists, then this <see cref="UUID"/> will reflect the remote one
        /// </summary>
        [JsonPropertyName("uuid")] public string UUID { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
