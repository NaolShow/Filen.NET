using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/dir/color"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri DirColorRoute => new Uri(FilenDomains.Gateway, "/v3/dir/color");

        /// <summary>
        /// Sets the specified folder's color and fetches <see cref="DirColorResponse"/>
        /// </summary>
        /// <returns>Fetched <see cref="DirColorResponse"/></returns>
        public async Task<DirColorResponse> SetDirColor(DirColorRequest request)
            => await (await HttpClient.PostAsJsonAsync(DirColorRoute, request)).Content.ReadFromJsonAsync<DirColorResponse>();

    }

}
