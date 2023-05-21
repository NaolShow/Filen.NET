using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received response from the <see cref="FilenAPI.DirRenameRoute"/>
    /// </summary>
    public struct DirRenameResponse {

        /// <summary>
        /// Represents the status of the <see cref="DirRenameResponse"/>
        /// </summary>
        [JsonPropertyName("status")] public bool Status { get; set; }
        /// <summary>
        /// Represents the code of the <see cref="DirRenameResponse"/> which could be:<br/>
        /// <br/>
        /// • <b>invalid_params</b>: Specified <see cref="DirRenameRequest.UUID"/> is not in a correct format<br/>
        /// • <b>folder_not_found</b>: No folder with the specified <see cref="DirRenameRequest.UUID"/> could be found<br/>
        /// • <b>folder_renamed</b>: Folder with the specified <see cref="DirRenameRequest.UUID"/> got renamed successfuly (<see cref="DirCreateData.UUID"/> can differ from <see cref="DirCreateRequest.UUID"/> if a folder with the same name already exists)<br/>
        /// </summary>
        [JsonPropertyName("code")] public string Code { get; set; }
        /// <summary>
        /// Represents the message associated with the <see cref="Code"/>
        /// </summary>
        [JsonPropertyName("message")] public string Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
