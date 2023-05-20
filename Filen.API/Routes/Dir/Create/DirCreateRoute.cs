using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/dir/create"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri DirCreateRoute => new Uri(FilenDomains.Gateway, "/v3/dir/create");

        /// <summary>
        /// Creates a directory and fetches <see cref="DirCreateData"/>
        /// </summary>
        /// <returns>Fetched <see cref="DirCreateData"/></returns>
        public async Task<DirCreateResponse> DirCreate(DirCreateRequest request)
            => await (await HttpClient.PostAsJsonAsync(DirCreateRoute, request)).Content.ReadFromJsonAsync<DirCreateResponse>();

    }

}
