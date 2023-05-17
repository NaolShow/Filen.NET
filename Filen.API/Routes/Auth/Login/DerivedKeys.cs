using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Filen.API {

    /// <summary>
    /// Contains the result of the user's password derivation (splitted in two parts to form the <see cref="MasterKey"/> and <see cref="AuthenticationKey"/>)<br/>
    /// As described in the section 2.1 of <see href="https://cdn.filen.io/whitepaper.pdf"/>
    /// </summary>
    public struct DerivedKeys {

        /// <summary>
        /// User's master key that is required to decrypt the master keys list to then decrypt metadata
        /// </summary>
        public string MasterKey { get; set; }
        /// <summary>
        /// User's authentication key that is required to login and acquire our API key
        /// </summary>
        public string AuthenticationKey { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

        /// <summary>
        /// Derives the user's <paramref name="password"/> with the <see cref="AuthInfoData.Salt"/>
        /// </summary>
        /// <param name="password">User's account password</param>
        /// <param name="authInfoData">Authentication data of the user's account</param>
        /// <returns><see cref="DerivedKeys"/> containing the <see cref="DerivedKeys.MasterKey"/> and the <see cref="DerivedKeys.AuthenticationKey"/></returns>
        public static DerivedKeys Derive(string password, AuthInfoData authInfoData) {

            if (authInfoData.AuthenticationVersion != 2)
                throw new NotSupportedException($"Authentication version '{authInfoData.AuthenticationVersion}' isn't supported at the moment");

            // Derive the user's password with the auth info salt
            using var pbkdf2 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(authInfoData.Salt), 200_000, HashAlgorithmName.SHA512);
            byte[] derivedBytes = pbkdf2.GetBytes(64);

            // Split the derived bytes to get the master and authentication keys
            byte[] masterKey = new byte[derivedBytes.Length / 2];
            byte[] authenticationKey = new byte[masterKey.Length];
            Array.Copy(derivedBytes, 0, masterKey, 0, masterKey.Length);
            Array.Copy(derivedBytes, masterKey.Length, authenticationKey, 0, authenticationKey.Length);

            // Convert both of those keys to hex strings (and hash the authentication key before hand)
            var masterKeyHex = Convert.ToHexString(masterKey).ToLower();
            var hashedAuthenticationKey = SHA512.HashData(Encoding.UTF8.GetBytes(Convert.ToHexString(authenticationKey).ToLower()));
            var authenticationKeyHex = Convert.ToHexString(hashedAuthenticationKey).ToLower();

            return new DerivedKeys() {
                MasterKey = masterKeyHex,
                AuthenticationKey = authenticationKeyHex
            };

        }

    }

}
