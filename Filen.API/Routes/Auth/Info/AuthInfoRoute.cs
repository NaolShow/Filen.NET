using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/auth/info"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri AuthInfoRoute => new Uri(FilenDomains.Gateway, "/v3/auth/info");

        /// <summary>
        /// Fetches <see cref="AuthInfoData"/> of a specific account's email
        /// (Fake <see cref="AuthInfoData.Salt"/> is returned if the <see cref="AuthInfoRequest.Email"/> isn't linked to any account to prevent brute force)
        /// </summary>
        /// <param name="request"><see cref="AuthInfoRequest"/> that is sent to the API</param>
        /// <returns>Fetched <see cref="AuthInfoResponse"/></returns>
        public async Task<AuthInfoResponse> GetAuthInfo(AuthInfoRequest request)
            => await (await HttpClient.PostAsJsonAsync(AuthInfoRoute, request)).Content.ReadFromJsonAsync<AuthInfoResponse>();

    }

}
