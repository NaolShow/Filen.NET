using System.Net.Http.Json;

namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/login"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri LoginRoute => new Uri(FilenDomains.Gateway, "/v3/login");

        /// <summary>
        /// Logins to <see href="https://filen.io"/> and set's the <see cref="FilenAPI"/> authorization header accordingly if successful
        /// </summary>
        /// <param name="request"><see cref="LoginRequest"/> that is sent to the API</param>
        /// <returns>Fetched <see cref="LoginResponse"/> from Filen</returns>
        public async Task<LoginResponse> Login(LoginRequest request) {
            LoginResponse loginResponse = await (await HttpClient.PostAsJsonAsync(LoginRoute, request)).Content.ReadFromJsonAsync<LoginResponse>();

            // If we logged-in successfully then set the authorization header to the api key
            if (loginResponse.Status)
                SetApiKey(loginResponse.Data.Value.ApiKey);

            return loginResponse;
        }

    }

}
