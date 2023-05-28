using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received response from the <see cref="FilenAPI.DirColorRoute"/>
    /// </summary>
    public struct DirColorResponse {

        /// <summary>
        /// Represents the status of the <see cref="DirContentResponse"/>
        /// </summary>
        [JsonPropertyName("status")] public bool Status { get; set; }
        /// <summary>
        /// Represents the code of the <see cref="DirContentResponse"/> which could be:<br/>
        /// <br/>
        /// • <b>invalid_params</b>: Specified <see cref="DirColor"/> isn't valid/supported (should never happen)<br/>
        /// • <b>folder_color_changed</b>: The color of the folder has been changed successfuly<br/>
        /// </summary>
        [JsonPropertyName("code")] public string Code { get; set; }
        /// <summary>
        /// Represents the message associated with the <see cref="Code"/>
        /// </summary>
        [JsonPropertyName("message")] public string Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
