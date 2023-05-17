using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents sent data to the <see cref="FilenAPI.AuthInfoRoute"/>
    /// </summary>
    public struct AuthInfoRequest {

        /// <summary>
        /// User's email address that has been used to create his account
        /// </summary>
        [JsonPropertyName("email")] public string Email { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="AuthInfoRequest"/> containing the user's email address
        /// </summary>
        /// <param name="email">User's email address that has been used to create his account</param>
        public AuthInfoRequest(string email) => Email = email;

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
