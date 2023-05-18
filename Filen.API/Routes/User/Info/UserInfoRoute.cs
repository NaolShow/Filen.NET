using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/user/info"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri UserInfoRoute => new Uri(FilenDomains.Gateway, "/v3/user/info");

        /// <summary>
        /// Fetches <see cref="UserInfoData"/> of the account that contains various informations
        /// </summary>
        /// <returns>Fetched <see cref="UserInfoData"/></returns>
        public async Task<UserInfoResponse> GetUserInfo() => await (await HttpClient.GetAsync(UserInfoRoute)).Content.ReadFromJsonAsync<UserInfoResponse>();

    }

}
