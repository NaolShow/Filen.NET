using Filen.API;

namespace Filen {

    /// <summary>
    /// High level API for <see href="https://filen.io"/>
    /// </summary>
    public partial class FilenClient {

        /// <summary>
        /// Represents the internal <see cref="FilenAPI"/> that is used to make requests to Filen
        /// </summary>
        public FilenAPI FilenAPI { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="FilenClient"/> that will help you interact with <see href="https://filen.io"/>
        /// </summary>
        /// <param name="sessionPath">Path to the session file that will (or currently) contains data that is required to use the API</param>
        public FilenClient(string? sessionPath = null) {

            // Initialize the underlying Filen API and save the session path
            FilenAPI = new FilenAPI();
            this.sessionPath = sessionPath;

            // TODO: Maybe add a way to easily change the session path and re-log?
            // => Is OK if you want to connect to two accounts of different people but it is forbidden to have more than one free account per person!

        }

    }

}