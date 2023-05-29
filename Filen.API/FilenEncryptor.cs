using System.Buffers;
using System.Security.Cryptography;
using System.Text;

namespace Filen.API {

    /// <summary>
    /// Provides methods that help to encrypt and decrypt file chunk data with the <see cref="AesGcm"/> algorithm<br/>
    /// (Without any memory allocation and with fast speed!)
    /// </summary>
    public class FilenEncryptor : IDisposable {

        /// <summary>
        /// Represents the size of the chunks content (after being decrypted)
        /// </summary>
        public const int ChunkSize = 1024 * 1024;
        /// <summary>
        /// Represents the size of the encrypted chunks (received from <see href="https://filen.io"/>)
        /// </summary>
        public const int EncryptedChunkSize = ChunkSize + IVSize + TagSize;

        /// <summary>
        /// Represents the size of the encryption key
        /// </summary>
        public const int KeySize = 32;
        /// <summary>
        /// Represents the size of the IV which is placed at the beginning of the encrypted data
        /// </summary>
        public const int IVSize = 12;
        /// <summary>
        /// Represents the size of the Tag which is placed at the end of the encrypted data
        /// </summary>
        public const int TagSize = 16;

        /// <summary>
        /// Represents the encryption/decryption key bytes<br/>
        /// (This is available only during the lifetime of <see cref="FilenEncryptor"/> because it is an array coming from <see cref="ArrayPool{T}"/>!)
        /// </summary>
        public byte[] Key { get; }

        private readonly AesGcm aesGcm;

        /// <summary>
        /// Initializes a new instance of <see cref="FilenEncryptor"/> which helps to <see cref="Encrypt(byte[], int, byte[], int, int)"/> and <see cref="Decrypt(byte[], int, byte[], int, int)"/> files chunks
        /// </summary>
        /// <param name="key">The key that is going to be used to encrypt/decrpt data (specify null to generate a random one)</param>
        public FilenEncryptor(string? key = null) {

            // Rent an array for the encryption key
            Key = ArrayPool<byte>.Shared.Rent(KeySize);

            // If there is no key then generate one
            if (key == null)
                FilenHelper.Fill(Key, 0, KeySize);
            // If there is a key then decode it with UTF8
            else Encoding.UTF8.GetBytes(key, 0, KeySize, Key, 0);

            // Initialize the AesGCM algorithm
            aesGcm = new AesGcm(Key);

        }

        /// <summary>
        /// Decrypts the specified <paramref name="input"/> and sets the decrypted bytes the <paramref name="output"/> buffer
        /// </summary>
        /// <param name="input">The input buffer that contains the encrypted data with it's IV and Tag</param>
        /// <param name="inputOffset">The offset from which the encrypted data is going to be read</param>
        /// <param name="output">The output buffer that will contain the decrypted data</param>
        /// <param name="outputOffset">The offset from which the decrypted data is going to be stored</param>
        /// <param name="count">The number of bytes of the encrypted content</param>
        /// <returns>The amount of bytes that has been written into the <paramref name="output"/> buffer from the specified <paramref name="outputOffset"/></returns>
        // Achieving ~3 700 MB/s on my computer with Ryzen 5 5600X, without a single allocation!
        public int Decrypt(byte[] input, int inputOffset, byte[] output, int outputOffset, int count) {

            // Substract the IV and the Tag to get the content size
            count = count - IVSize - TagSize;

            // Extract the IV, message and tag from the input
            Span<byte> iv = input.AsSpan(inputOffset, IVSize);
            Span<byte> encryptedMessage = input.AsSpan(inputOffset + IVSize, count);
            Span<byte> tag = input.AsSpan(inputOffset + IVSize + count, TagSize);

            Span<byte> decryptedMessage = output.AsSpan(outputOffset, count);

            // Decrypt and copy the decrypted data into the output buffer
            aesGcm.Decrypt(iv, encryptedMessage, tag, decryptedMessage);
            return count;

        }

        /// <summary>
        /// Decrypts the specified <paramref name="buffer"/> and sets the decrypted bytes into the same buffer
        /// </summary>
        /// <param name="buffer">The buffer that contains the encrypted data with it's IV and Tag</param>
        /// <param name="offset">The offset from which the encrypted data is going to be read and stored</param>
        /// <inheritdoc cref="Decrypt(byte[], int, byte[], int, int)"/>
        public int Decrypt(byte[] buffer, int offset, int count) => Decrypt(buffer, offset, buffer, offset, count);

        /// <summary>
        /// Encrypts the specified <paramref name="input"/> and sets the encrypted bytes the <paramref name="output"/> buffer (including it's IV and Tag)
        /// </summary>
        /// <param name="input">The input buffer that contains the unencrypted data</param>
        /// <param name="inputOffset">The offset from which the unencrypted data is going to be read</param>
        /// <param name="output">The output buffer that will contain the encrypted data</param>
        /// <param name="outputOffset">The offset from which the encrypted data is going to be stored</param>
        /// <param name="count">The number of bytes of the unencrypted content</param>
        /// <returns>The amount of bytes that has been written into the <paramref name="output"/> buffer from the specified <paramref name="outputOffset"/></returns>
        // Achieving ~2 500 MB/s on my computer with Ryzen 5 5600X, without a single allocation!
        public int Encrypt(byte[] input, int inputOffset, byte[] output, int outputOffset, int count) {

            // If both input and output buffers are the same then move the content to not overwrite part of it when generating the IV
            if (ReferenceEquals(input, output))
                Buffer.BlockCopy(input, inputOffset, input, inputOffset + IVSize, count);

            // Extract the IV, message and tag from the output
            Span<byte> iv = output.AsSpan(outputOffset, IVSize);
            Span<byte> decryptedMessage = input.AsSpan(inputOffset, count);
            Span<byte> tag = output.AsSpan(outputOffset + IVSize + count, TagSize);

            Span<byte> encryptedMessage = output.AsSpan(outputOffset + IVSize, count);

            // Get a random and cryptographically secure IV and Tag
            FilenHelper.Fill(output, outputOffset, IVSize);
            RandomNumberGenerator.Fill(tag);

            // Encrypt the bytes and copy the IV, encrypted content and Tag into the output buffer
            aesGcm.Encrypt(iv, decryptedMessage, encryptedMessage, tag);
            return IVSize + count + TagSize;

        }

        /// <summary>
        /// Decrypts the specified <paramref name="buffer"/> and sets the decrypted bytes into the same buffer
        /// </summary>
        /// <param name="buffer">The buffer that contains the unencrypted data</param>
        /// <param name="offset">The offset from which the data is going to be read and stored</param>
        /// <inheritdoc cref="Encrypt(byte[], int, byte[], int, int)"/>
        public int Encrypt(byte[] buffer, int offset, int count) => Encrypt(buffer, offset, buffer, offset, count);

        /// <summary>
        /// Disposes the internal <see cref="Key"/> array and <see cref="AesGcm"/> instance
        /// </summary>
        public void Dispose() {
            ArrayPool<byte>.Shared.Return(Key);
            aesGcm.Dispose();
            GC.SuppressFinalize(this);
        }

    }

}