using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received response from the <see cref="FilenAPI.DirPresentRoute"/>
    /// </summary>
    public struct DirPresentResponse {

        /// <summary>
        /// Represents the status of the <see cref="DirPresentResponse"/>
        /// </summary>
        [MemberNotNullWhen(true, nameof(Data))]
        [JsonPropertyName("status")] public bool Status { get; set; }
        /// <summary>
        /// Represents the code of the <see cref="DirPresentResponse"/> which could be:<br/>
        /// <br/>
        /// • <b>invalid_params</b>: Specified <see cref="DirPresentRequest.UUID"/> is not in a correct format<br/>
        /// • <b>folder_does_not_exist</b>: No folder with the specified <see cref="DirPresentRequest.UUID"/> exists (however, a file with this UUID can still exist!)<br/>
        /// • <b>folder_exists</b>: A folder with the specified <see cref="DirPresentRequest.UUID"/> exists<br/>
        /// </summary>
        [JsonPropertyName("code")] public string Code { get; set; }
        /// <summary>
        /// Represents the message associated with the <see cref="Code"/>
        /// </summary>
        [JsonPropertyName("message")] public string Message { get; set; }
        /// <summary>
        /// <inheritdoc cref="DirPresentData"/><br/>
        /// (Only available when <see cref="Status"/> is true)
        /// </summary>
        [JsonPropertyName("data")] public DirPresentData? Data { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
