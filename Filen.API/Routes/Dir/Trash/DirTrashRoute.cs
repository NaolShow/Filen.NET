using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/dir/trash"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri DirTrashRoute => new Uri(FilenDomains.Gateway, "/v3/dir/trash");

        /// <summary>
        /// Trashes a folder and fetches the <see cref="DirTrashResponse"/>
        /// </summary>
        /// <returns>Fetched <see cref="DirTrashResponse"/></returns>
        public async Task<DirTrashResponse> DirTrash(DirTrashRequest request)
            => await (await HttpClient.PostAsJsonAsync(DirTrashRoute, request)).Content.ReadFromJsonAsync<DirTrashResponse>();

    }

}
