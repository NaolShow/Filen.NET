using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/dir/rename"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri DirRenameRoute => new Uri(FilenDomains.Gateway, "/v3/dir/rename");

        /// <summary>
        /// Renames a folder and fetches the <see cref="DirRenameResponse"/>
        /// </summary>
        /// <returns>Fetched <see cref="DirRenameResponse"/></returns>
        public async Task<DirRenameResponse> DirRename(DirRenameRequest request)
            => await (await HttpClient.PostAsJsonAsync(DirRenameRoute, request)).Content.ReadFromJsonAsync<DirRenameResponse>();

    }

}
