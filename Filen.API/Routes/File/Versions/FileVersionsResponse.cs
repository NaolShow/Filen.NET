using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received response from the <see cref="FilenAPI.FileVersionsRoute"/>
    /// </summary>
    public struct FileVersionsResponse {

        /// <summary>
        /// Represents the status of the <see cref="FileVersionsResponse"/>
        /// </summary>
        [MemberNotNullWhen(true, nameof(Data))]
        [JsonPropertyName("status")] public bool Status { get; set; }
        /// <summary>
        /// Represents the code of the <see cref="FileVersionsResponse"/> which could be:<br/>
        /// <br/>
        /// • <b>invalid_params</b>: Specified <see cref="FileVersionsRequest.UUID"/> isn't in a valid format<br/>
        /// • <b>file_not_found</b>: Specified file with <see cref="FileVersionsRequest.UUID"/> doesn't exist<br/>
        /// • <b>file_versions_fetched</b>: The file with <see cref="FileVersionsRequest.UUID"/> exists and its versions has been fetched<br/>
        /// </summary>
        [JsonPropertyName("code")] public string Code { get; set; }
        /// <summary>
        /// Represents the message associated with the <see cref="Code"/>
        /// </summary>
        [JsonPropertyName("message")] public string Message { get; set; }
        /// <summary>
        /// <inheritdoc cref="FileVersionsData"/><br/>
        /// (Only available when <see cref="Status"/> is true)
        /// </summary>
        [JsonPropertyName("data")] public FileVersionsData? Data { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
