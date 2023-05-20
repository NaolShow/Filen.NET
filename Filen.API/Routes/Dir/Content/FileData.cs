using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Represents received data from the <see cref="FilenAPI.DirContentRoute"/>
    /// </summary>
    public struct FileData {

        /// <summary>
        /// Represents the size (in bytes) of a single chunk of file
        /// </summary>
        public const int ChunkSize = 1024 * 1024;

        /// <summary>
        /// Represents the unique ID of the file
        /// </summary>
        [JsonPropertyName("uuid")] public string UUID { get; set; }
        /// <summary>
        /// Represents the encrypted <see cref="FileMetadata"/> of the file
        /// </summary>
        [JsonPropertyName("metadata")] public string EncryptedMetadata { get; set; }

        /// <summary>
        /// Represents the bucket in which the file is available to download
        /// </summary>
        [JsonPropertyName("bucket")] public string Bucket { get; set; }
        /// <summary>
        /// Represents the region in which the file is available to download
        /// </summary>
        [JsonPropertyName("region")] public string Region { get; set; }
        /// <summary>
        /// Represents the number of chunks that the file contains (one chunk = <see cref="ChunkSize"/> bytes)
        /// </summary>
        [JsonPropertyName("chunks")] public int Chunks { get; set; }
        /// <summary>
        /// Represents the total size of the file
        /// </summary>
        [JsonPropertyName("size")] public long Size { get; set; }

        /// <summary>
        /// Represents the version of the encryption protocol of the file
        /// </summary>
        [JsonPropertyName("version")] public int Version { get; set; }
        /// <summary>
        /// (Utility not determined, it looks like it was used to delete files)
        /// </summary>
        [JsonPropertyName("rm")] public string RM { get; set; }

        /// <summary>
        /// Determines the file's parent UUID
        /// </summary>
        [JsonPropertyName("parent")] public string Parent { get; set; }
        /// <summary>
        /// Determines the time at which the file has been uploaded<br/>
        /// (Use <see cref="DateTimeOffset.FromUnixTimeSeconds(long)"/> to get the actual date)
        /// </summary>
        [JsonPropertyName("timestamp")] public long Timestamp { get; set; }
        /// <summary>
        /// Determines if the file is favorited or not
        /// </summary>
        [JsonPropertyName("favorited")] public int Favorited { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);

    }

    /// <summary>
    /// Represents the decrypted <see cref="FileData.EncryptedMetadata"/>
    /// </summary>
    public struct FileMetadata {

        /// <summary>
        /// Represents the name of the file
        /// </summary>
        [JsonPropertyName("name")] public string Name { get; set; }
        /// <inheritdoc cref="FileData.Size"/>
        [JsonPropertyName("size")] public long Size { get; set; }
        /// <summary>
        /// Represents the mime type of the file ('video/mp4', ...)
        /// </summary>
        [JsonPropertyName("mime")] public string MimeType { get; set; }
        /// <summary>
        /// Represents the file's encryption key
        /// </summary>
        [JsonPropertyName("key")] public string EncryptionKey { get; set; }
        /// <summary>
        /// Determines the time at which the file has been modified<br/>
        /// (This is different than <see cref="FileData.Timestamp"/>, this one determines the time the file has been modified for the last time and not uploaded!)<br/>
        /// <br/>
        /// (Use <see cref="DateTimeOffset.FromUnixTimeMilliseconds(long)"/> to get the actual date)
        /// </summary>
        [JsonPropertyName("lastModified")] public long LastModified { get; set; }

    }

}
