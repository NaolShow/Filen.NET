using System.Security.Cryptography;
using System.Text;

namespace Filen.API {

    /// <summary>
    /// Provides easy way to encrypt and decrypt data received or sent to <see href="https://filen.io"/><br/>
    /// All of them are described in the <see href="https://cdn.filen.io/whitepaper.pdf"/>
    /// </summary>
    public static class FilenEncryption {

        /// <inheritdoc cref="DecryptMetadata(string, Span{string})"/>
        public static string? DecryptMetadata(string metadata, params string[] masterKeys) => DecryptMetadata(metadata, masterKeys.AsSpan());

        /// <summary>
        /// Tries to decrypt the specified metadata string with all the specified master keys (from the latest to the first one)
        /// </summary>
        /// <param name="metadata">The encrypted metadata string</param>
        /// <param name="masterKeys">The master keys that could have encrypted this metadata</param>
        /// <returns>Decrypted metadata string or null if none of the specified master keys could decrypt it</returns>
        public static string? DecryptMetadata(string metadata, Span<string> masterKeys) {
            for (int i = masterKeys.Length - 1; i >= 0; i--) {

                // Derive the master key that is used as the decryption key
                byte[] masterKeyBytes = Encoding.UTF8.GetBytes(masterKeys[i]);
                byte[] decryptionKeyBytes = Rfc2898DeriveBytes.Pbkdf2(masterKeyBytes, masterKeyBytes, 1, HashAlgorithmName.SHA512, 32);
                using AesGcm aesGcm = new AesGcm(decryptionKeyBytes);

                // TODO: Metadata version and decrypt accordingly

                // Get the IV at the beginning of the metadata
                byte[] iv = Encoding.UTF8.GetBytes(metadata[3..15]);

                // Get the content (right part without IV), and extract the tag and the cipher data
                byte[] content = Convert.FromBase64String(metadata[15..]);
                byte[] tag = content[^16..];
                byte[] cipher = content[..^16];

                try {

                    // Decrypt the metadata
                    aesGcm.Decrypt(iv, cipher, tag, cipher);
                    return Encoding.UTF8.GetString(cipher);

                } catch (CryptographicException) {
                    // Key isn't correct then go to the next one
                }

            }
            return null;
        }

        /// <summary>
        /// Tries to decrypt the specified master keys string (that is a string with concatenated master keys) and return them in an array<br/>
        /// (If the specified master key used to decrypt is incorrect then return null)
        /// </summary>
        /// <param name="masterKeys">The encrypted metadata string that contains all the master keys</param>
        /// <param name="masterKey">The user's master key that is used to decrypt the metadata</param>
        /// <returns></returns>
        public static string[]? DecryptMasterKeys(string masterKeys, string masterKey) => DecryptMetadata(masterKeys, masterKey)?.Split('|');

    }

}
