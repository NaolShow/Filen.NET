using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received response from the <see cref="FilenAPI.DirCreateRoute"/>
    /// </summary>
    public struct DirCreateResponse {

        /// <summary>
        /// Represents the status of the <see cref="DirCreateResponse"/>
        /// </summary>
        [MemberNotNullWhen(true, nameof(Data))]
        [JsonPropertyName("status")] public bool Status { get; set; }
        /// <summary>
        /// Represents the code of the <see cref="DirCreateResponse"/> which could be:<br/>
        /// <br/>
        /// • <b>invalid_params</b>: Specified <see cref="DirCreateRequest"/> values are not in a correct format<br/>
        /// • <b>parent_folder_not_found</b>: No folder with the specified <see cref="DirCreateRequest.UUID"/> could be found<br/>
        /// • <b>internal_error</b>: A folder with the same <see cref="DirCreateRequest.UUID"/> but with another name has been found<br/>
        /// • <b>folder_created</b>: Folder with the specified <see cref="DirCreateRequest.UUID"/> and <see cref="DirCreateRequest.EncryptedMetadata"/> got created successfuly<br/>
        /// </summary>
        [JsonPropertyName("code")] public string Code { get; set; }
        /// <summary>
        /// Represents the message associated with the <see cref="Code"/>
        /// </summary>
        [JsonPropertyName("message")] public string Message { get; set; }
        /// <summary>
        /// <inheritdoc cref="DirCreateData"/><br/>
        /// (Only available when <see cref="Status"/> is true)
        /// </summary>
        [JsonPropertyName("data")] public DirCreateData? Data { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
