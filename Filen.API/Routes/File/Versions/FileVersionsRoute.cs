using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/file/versions"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri FileVersionsRoute => new Uri(FilenDomains.Gateway, "/v3/file/versions");

        /// <summary>
        /// Fetches <see cref="FileVersionsResponse"/>
        /// </summary>
        /// <returns>Fetched <see cref="FileVersionsResponse"/></returns>
        public async Task<FileVersionsResponse> FileVersions(FileVersionsRequest request)
            => await (await HttpClient.PostAsJsonAsync(FileVersionsRoute, request)).Content.ReadFromJsonAsync<FileVersionsResponse>();

    }

}
