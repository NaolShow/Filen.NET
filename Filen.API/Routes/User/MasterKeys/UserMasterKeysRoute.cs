using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/user/masterKeys"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri UserMasterKeysRoute => new Uri(FilenDomains.Gateway, "/v3/user/masterKeys");

        /// <summary>
        /// Fetches <see cref="UserMasterKeysData"/> of the account that contains all it's master keys
        /// </summary>
        /// <returns>Fetched <see cref="UserMasterKeysData"/></returns>
        public async Task<UserMasterKeysResponse> GetMasterKeys()
            => await (await HttpClient.PostAsJsonAsync(UserMasterKeysRoute, new {
                // In theory we should send the user's encrypted master keys here, but it works without it and the web drive client
                // Doesn't even send the correct encrypted master keys, so let's just send some random data for now
                masterKeys = "x"
            })).Content.ReadFromJsonAsync<UserMasterKeysResponse>();

    }

}
