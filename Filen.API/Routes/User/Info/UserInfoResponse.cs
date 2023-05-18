using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received response from the <see cref="FilenAPI.UserInfoRoute"/>
    /// </summary>
    public struct UserInfoResponse {

        /// <summary>
        /// Represents the status of the <see cref="UserInfoResponse"/>
        /// </summary>
        [MemberNotNullWhen(true, nameof(Data))]
        [JsonPropertyName("status")] public bool Status { get; set; }
        /// <summary>
        /// Represents the code of the <see cref="UserInfoResponse"/>
        /// </summary>
        [JsonPropertyName("code")] public string Code { get; set; }
        /// <summary>
        /// Represents the message associated with the <see cref="Code"/>
        /// </summary>
        [JsonPropertyName("message")] public string Message { get; set; }
        /// <summary>
        /// <inheritdoc cref="UserInfoData"/><br/>
        /// (Only available when <see cref="Status"/> is true)
        /// </summary>
        [JsonPropertyName("data")] public UserInfoData? Data { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
