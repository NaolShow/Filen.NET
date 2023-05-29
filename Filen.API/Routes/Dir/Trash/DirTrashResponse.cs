using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received response from the <see cref="FilenAPI.DirTrashRoute"/>
    /// </summary>
    public struct DirTrashResponse {

        /// <summary>
        /// Represents the status of the <see cref="DirTrashResponse"/>
        /// </summary>
        [JsonPropertyName("status")] public bool Status { get; set; }
        /// <summary>
        /// Represents the code of the <see cref="DirTrashResponse"/> which could be:<br/>
        /// <br/>
        /// • <b>invalid_params</b>: Specified <see cref="DirTrashRequest.UUID"/> isn't in a valid format<br/>
        /// • <b>file_not_found</b>: Specified folder with <see cref="DirTrashRequest.UUID"/> doesn't exist<br/>
        /// • <b>folder_trashed</b>: The folder has been trashed successfuly (same if the folder was already in trash)<br/>
        /// </summary>
        [JsonPropertyName("code")] public string Code { get; set; }
        /// <summary>
        /// Represents the message associated with the <see cref="Code"/>
        /// </summary>
        [JsonPropertyName("message")] public string Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
