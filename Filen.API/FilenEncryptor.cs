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
        // Achieving ~2 500 MB/s on my computer with Ryzen 5 5600X, without a single allocation!
        public int Decrypt(byte[] input, int inputOffset, byte[] output, int outputOffset, int count) {

            // Substract the IV and the Tag to get the content size
            count = count - IVSize - TagSize;

            // Rent the arrays for the IV, message and tag
            byte[] ivArray = ArrayPool<byte>.Shared.Rent(IVSize);
            byte[] messageArray = ArrayPool<byte>.Shared.Rent(count);
            byte[] tagArray = ArrayPool<byte>.Shared.Rent(TagSize);

            // Copy the data into the arrays
            Buffer.BlockCopy(input, inputOffset, ivArray, 0, IVSize);
            Buffer.BlockCopy(input, inputOffset + IVSize, messageArray, 0, count);
            Buffer.BlockCopy(input, inputOffset + IVSize + count, tagArray, 0, TagSize);

            // Use non allocation Span to only extract a part of the arrays (AesGcm doesn't support buffer offset and count..)
            // => This is needed because array pool doesn't necessarily give us an array of the requested size
            Span<byte> iv = ivArray.AsSpan(0, IVSize);
            Span<byte> message = messageArray.AsSpan(0, count);
            Span<byte> tag = tagArray.AsSpan(0, TagSize);

            // Decrypt and copy the decrypted data into the input buffer
            aesGcm.Decrypt(iv, message, tag, message);
            Buffer.BlockCopy(messageArray, 0, output, outputOffset, count);

            // Return all the arrays
            ArrayPool<byte>.Shared.Return(ivArray);
            ArrayPool<byte>.Shared.Return(messageArray);
            ArrayPool<byte>.Shared.Return(tagArray);
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

            // Rent the arrays for the IV and tag
            byte[] ivArray = ArrayPool<byte>.Shared.Rent(IVSize);
            byte[] messageArray = ArrayPool<byte>.Shared.Rent(count);
            byte[] tagArray = ArrayPool<byte>.Shared.Rent(TagSize);

            // Use non allocation Span to only extract a part of those arrays (see why in the decrypt method)
            Span<byte> iv = ivArray.AsSpan(0, IVSize);
            Span<byte> message = messageArray.AsSpan(0, count);
            Span<byte> tag = tagArray.AsSpan(0, TagSize);

            // Copy the unencrypted data and fill the IV and Tag with cryptographically strong random bytes
            FilenHelper.Fill(ivArray, 0, IVSize);
            Buffer.BlockCopy(input, inputOffset, messageArray, 0, count);
            RandomNumberGenerator.Fill(tag);

            // Encrypt the bytes and copy the IV, encrypted content and Tag into the output buffer
            aesGcm.Encrypt(iv, message, message, tag);
            Buffer.BlockCopy(ivArray, 0, output, outputOffset, IVSize);
            Buffer.BlockCopy(messageArray, 0, output, outputOffset + IVSize, count);
            Buffer.BlockCopy(tagArray, 0, output, outputOffset + IVSize + count, TagSize);

            // Return all the arrays
            ArrayPool<byte>.Shared.Return(ivArray);
            ArrayPool<byte>.Shared.Return(messageArray);
            ArrayPool<byte>.Shared.Return(tagArray);
            return count + IVSize + TagSize;

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
        }

    }

}