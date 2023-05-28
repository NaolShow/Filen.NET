using System.Security.Cryptography;
using System.Text;

namespace Filen.API {

    /// <summary>
    /// Provides easy way to encrypt and decrypt data received or sent to <see href="https://filen.io"/><br/>
    /// All of them are described in the <see href="https://cdn.filen.io/whitepaper.pdf"/>
    /// </summary>
    public static class FilenEncryption {

        /// <summary>
        /// Represents available chars for an IV
        /// </summary>
        const string IVChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// Fills the specified array with a cryptographically secure bytes that can be encoded into a readable UTF8 string<br/>
        /// (This is mostly used to generate IVs that can then be used with <see href="https://filen.io"/>)
        /// </summary>
        /// <param name="array">The byte array that will get filled with cryptographically secure bytes</param>
        public static void Fill(byte[] array, int offset, int count) {
            for (int i = offset; i < count; i++)
                array[i] = (byte)IVChars[RandomNumberGenerator.GetInt32(IVChars.Length)];
        }

        /// <summary>
        /// Encrypts the specified metadata string with the specified master key
        /// </summary>
        /// <param name="metadata">The metadata to encrypt</param>
        /// <param name="masterKey">The master key used to encrypt the metadata</param>
        /// <returns>The encrypted metadata string</returns>
        public static string EncryptMetadata(string metadata, string masterKey) {

            // Derive the master key and initialize the aes gcm instance
            byte[] masterKeyBytes = Encoding.UTF8.GetBytes(masterKey);
            byte[] encryptionKeyBytes = Rfc2898DeriveBytes.Pbkdf2(masterKeyBytes, masterKeyBytes, 1, HashAlgorithmName.SHA512, 32);
            using AesGcm aesGcm = new AesGcm(encryptionKeyBytes);

            // Generate a cryptographically secure IV and Tag
            char[] ivChars = new char[12];
            for (int i = 0; i < ivChars.Length; i++)
                ivChars[i] = IVChars[RandomNumberGenerator.GetInt32(IVChars.Length)];
            string ivString = new string(ivChars);
            byte[] iv = Encoding.UTF8.GetBytes(ivString);
            byte[] tag = RandomNumberGenerator.GetBytes(16);

            // Encrypt the content
            byte[] content = Encoding.UTF8.GetBytes(metadata);
            aesGcm.Encrypt(iv, content, content, tag);

            // Concatenate the encrypted content and the tag
            byte[] result = new byte[content.Length + tag.Length];
            Buffer.BlockCopy(content, 0, result, 0, content.Length);
            Buffer.BlockCopy(tag, 0, result, content.Length, tag.Length);

            // 002 for the metadata version, and then the content/tag encoded in base64
            return $"002{ivString}{Convert.ToBase64String(result)}";

        }

        /// <inheritdoc cref="DecryptMetadata(string, IReadOnlyList{string})"/>
        public static string? DecryptMetadata(string metadata, params string[] masterKeys) => DecryptMetadata(metadata, (IReadOnlyList<string>)masterKeys);

        /// <summary>
        /// Tries to decrypt the specified metadata string with all the specified master keys (from the latest to the first one)
        /// </summary>
        /// <param name="metadata">The encrypted metadata string</param>
        /// <param name="masterKeys">The master keys that could have encrypted this metadata</param>
        /// <returns>Decrypted metadata string or null if none of the specified master keys could decrypt it</returns>
        public static string? DecryptMetadata(string metadata, IReadOnlyList<string> masterKeys) {
            for (int i = masterKeys.Count - 1; i >= 0; i--) {

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

        /// <summary>
        /// Hashes the specified <paramref name="name"/> with <see cref="SHA512"/> and then <see cref="SHA1"/> and then return it's hex string required for <see cref="DirCreateRequest"/>
        /// </summary>
        /// <param name="name">The plain text name that will be hashed</param>
        /// <returns>Hex encoded string representing the hashed name</returns>
        public static string HashName(string name) {

            byte[] sha512 = SHA512.HashData(Encoding.UTF8.GetBytes(name.ToLower()));
            string sha512Hex = Convert.ToHexString(sha512).ToLower();

            byte[] sha1 = SHA1.HashData(Encoding.UTF8.GetBytes(sha512Hex));
            return Convert.ToHexString(sha1).ToLower();

        }

    }

}
