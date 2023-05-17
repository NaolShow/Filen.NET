using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents sent data to the <see cref="FilenAPI.LoginRoute"/>
    /// </summary>
    public struct LoginRequest {

        /// <summary>
        /// User's email address that has been used to create his account
        /// </summary>
        [JsonPropertyName("email")] public string Email { get; set; }
        /// <inheritdoc cref="DerivedKeys.AuthenticationKey"/>
        [JsonPropertyName("password")] public string AuthenticationKey { get; set; }
        /// <summary>
        /// User's two factor code (or "XXXXXX" if not configured)
        /// </summary>
        [JsonPropertyName("twoFactorCode")] public string TwoFactorCode { get; set; }
        /// <inheritdoc cref="AuthInfoData.AuthenticationVersion"/>
        [JsonPropertyName("authVersion")] public int AuthenticationVersion { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="AuthInfoRequest"/> containing the user's email address
        /// </summary>
        /// <param name="authInfoData">User's <see cref="AuthInfoData"/> acquired from <see cref="FilenAPI.GetAuthInfo(AuthInfoRequest)"/></param>
        /// <param name="derivedKeys">User's <see cref="DerivedKeys"/> acquired from <see cref="DerivedKeys.Derive(string, AuthInfoData)"/></param>
        /// <param name="twoFactorCode">User's two factor code acquired from it's two factor app or "XXXXXX" if not configured</param>
        public LoginRequest(AuthInfoData authInfoData, DerivedKeys derivedKeys, string twoFactorCode = "XXXXXX") {
            Email = authInfoData.Email;
            AuthenticationVersion = authInfoData.AuthenticationVersion;
            AuthenticationKey = derivedKeys.AuthenticationKey;
            TwoFactorCode = twoFactorCode;
        }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

}
