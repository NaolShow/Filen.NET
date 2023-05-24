using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/dir/present"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri DirPresentRoute => new Uri(FilenDomains.Gateway, "/v3/dir/present");

        /// <summary>
        /// Checks if a folder exists with the specified <see cref="DirPresentRequest.UUID"/> and fetches the <see cref="DirPresentData"/>
        /// </summary>
        /// <returns>Fetched <see cref="DirPresentData"/></returns>
        public async Task<DirPresentResponse> DirPresent(DirPresentRequest request)
            => await (await HttpClient.PostAsJsonAsync(DirPresentRoute, request)).Content.ReadFromJsonAsync<DirPresentResponse>();

    }

}
