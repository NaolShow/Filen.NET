using System.Net.Http.Headers;

namespace Filen.API {

    /// <summary>
    /// Low level API for <see href="https://filen.io"/>
    /// </summary>
    public partial class FilenAPI {

        /// <summary>
        /// Represents the internal <see cref="HttpClient"/> that is used to make requests to <see href="https://filen.io"/><br/>
        /// (This <see cref="HttpClient"/> will get it's Authorization header be automatically set with the correct api key after <see cref="Login(Messages.LoginRequest)"/>)
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="FilenAPI"/> that will let you interact with <see href="https://filen.io"/> API in a "low level" manner
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> that should be used or null to get a new one</param>
        public FilenAPI(HttpClient? httpClient = null) {

            // Use the specified HttpClient if not null or initialize a new one
            HttpClient = httpClient ?? new HttpClient();

            // By default set the authorization header to null
            SetApiKey("null");
            HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Filen.NET");

        }

        /// <summary>
        /// Sets the specified <paramref name="apiKey"/> in the <see cref="HttpClient"/> default authorization request header
        /// </summary>
        /// <param name="apiKey">The api key to access Filen API acquired from <see cref="Login(LoginRequest)"/></param>
        public void SetApiKey(string apiKey) => HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

    }

}