namespace Filen.API {

    public partial class FilenAPI {

        /// <summary>
        /// Points towards the route <see href="/v3/trash/empty"/> with a random <see cref="FilenDomains.Gateway"/>
        /// </summary>
        public static Uri TrashEmptyRoute => new Uri(FilenDomains.Gateway, "/v3/trash/empty");

        /// <summary>
        /// Empties the trash from any files and folders (cannot be restored!)
        /// </summary>
        public async Task TrashEmpty()
            => await HttpClient.PostAsync(TrashEmptyRoute, null);

    }

}
