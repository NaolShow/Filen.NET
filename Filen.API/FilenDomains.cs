namespace Filen {

    /// <summary>
    /// Provides easy access to the different <see href="https://filen.io"/> domains (for API, file download and upload)<br/>
    /// These values can be fetched directly from the Filen products source <see href="https://github.com/FilenCloudDienste/filen-drive/blob/master/src/lib/constants.ts"/>
    /// </summary>
    public class FilenDomains {

        /// <summary>
        /// Possible domains that can be use to make API calls to <see href="https://filen.io"/>
        /// </summary>
        public static IReadOnlyList<Uri> Gateways { get; } = new Uri[] {
            new Uri("https://gateway.filen.io"),
            new Uri("https://gateway.filen.net"),
            new Uri("https://gateway.filen-1.net"),
            new Uri("https://gateway.filen-2.net"),
            new Uri("https://gateway.filen-3.net"),
            new Uri("https://gateway.filen-4.net"),
            new Uri("https://gateway.filen-5.net"),
            new Uri("https://gateway.filen-6.net")
        };

        /// <summary>
        /// Gets a random gateway domain from the <see cref="Gateways"/>
        /// </summary>
        public static Uri Gateway => Gateways[Random.Shared.Next(Gateways.Count)];

        /// <summary>
        /// Possible domains that can be use to download files from <see href="https://filen.io"/>
        /// </summary>
        public static IReadOnlyList<Uri> Downloads { get; } = new Uri[] {
            new Uri("https://down.filen.io"),
            new Uri("https://down.filen.net"),
            new Uri("https://down.filen-1.net"),
            new Uri("https://down.filen-2.net"),
            new Uri("https://down.filen-3.net"),
            new Uri("https://down.filen-4.net"),
            new Uri("https://down.filen-5.net"),
            new Uri("https://down.filen-6.net")
        };

        /// <summary>
        /// Gets a random download domain from the <see cref="Downloads"/>
        /// </summary>
        public static Uri Download => Downloads[Random.Shared.Next(Downloads.Count)];

        /// <summary>
        /// Builds the download link to the <see cref="Upload"/> with <paramref name="uuid"/> that is in the specified <paramref name="region"/> and <paramref name="bucket"/><br/>
        /// (You must add manually at the end the chunk index that you want to download)
        /// </summary>
        /// <param name="region">Region of the server that contains the <see cref="Upload"/></param>
        /// <param name="bucket">Bucket that contains the <see cref="Upload"/></param>
        /// <param name="uuid"><see cref="Upload.UUID"/></param>
        /// <returns><see cref="Uri"/> of format <see cref="Download"/>/<paramref name="region"/>/<paramref name="bucket"/>/<paramref name="uuid"/></returns>
        public static Uri BuildDownload(string region, string bucket, string uuid) => new Uri(Download, $"{region}/{bucket}/{uuid}");

        /// <summary>
        /// Possible domains that can be use to upload files to <see href="https://filen.io"/>
        /// </summary>
        public static IReadOnlyList<Uri> Uploads { get; } = new Uri[] {
            new Uri("https://ingest.filen.io"),
            new Uri("https://ingest.filen.net"),
            new Uri("https://ingest.filen-1.net"),
            new Uri("https://ingest.filen-2.net"),
            new Uri("https://ingest.filen-3.net"),
            new Uri("https://ingest.filen-4.net"),
            new Uri("https://ingest.filen-5.net"),
            new Uri("https://ingest.filen-6.net")
        };

        /// <summary>
        /// Gets a random upload domain from the <see cref="Uploads"/>
        /// </summary>
        public static Uri Upload => Uploads[Random.Shared.Next(Uploads.Count)];

    }

}
