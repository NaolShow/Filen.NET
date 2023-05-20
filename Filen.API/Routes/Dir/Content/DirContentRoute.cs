using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/dir/content"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri DirContentRoute => new Uri(FilenDomains.Gateway, "/v3/dir/content");

        /// <summary>
        /// Fetches <see cref="DirContentData"/> of the specified folder
        /// </summary>
        /// <returns>Fetched <see cref="DirContentData"/></returns>
        public async Task<DirContentResponse> GetDirContent(DirContentRequest request)
            => await (await HttpClient.PostAsJsonAsync(DirContentRoute, request)).Content.ReadFromJsonAsync<DirContentResponse>();

    }

}
