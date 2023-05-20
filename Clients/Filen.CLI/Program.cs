using Filen.API;
using System.Text.Json;

namespace Filen.CLI {

    /// <summary>
    /// Sample program that will (in the future) transform into a fully fledge CLI
    /// </summary>
    internal class Program {

        private static FilenClient filenClient;

        /// <summary>
        /// Asks for data for the user to input in the console with a message
        /// </summary>
        /// <param name="message">The message to show to the user when asking for data</param>
        /// <returns>The inputted data by the user</returns>
        private static string Ask(string message) {
            Console.WriteLine(message);
            Console.Write("> ");
            return Console.ReadLine();
        }

        static void Main(string[] args) {

            // Initialize the FilenClient that will store it's session into "filenSession.json" file
            filenClient = new FilenClient("filenSession.json");
            filenClient.OnRequireCredential += (element) => element switch {
                FilenCredential.Email => Ask("What is your email?"),
                FilenCredential.Password => Ask("What is your password?"),
                FilenCredential.Code => Ask("What is your 2FA code?"),
                _ => throw new InvalidOperationException()
            };

            // Wait for the program to finish
            MainAsync().GetAwaiter().GetResult();
            Console.ReadLine();

        }

        private static async Task MainAsync() {

            // Login into https://filen.io
            await filenClient.Login();

            // Ask for the user informations and write the user's email (with the underlying FilenAPI instance)
            UserInfoResponse userInfoResponse = await filenClient.FilenAPI.GetUserInfo();
            Console.WriteLine($"{userInfoResponse.Data.Value.Email}");

            // Get the content of the base folder and write all the folder's name
            Console.WriteLine($"Folders that are inside your base folder:");
            DirContentResponse dirContentResponse = await filenClient.FilenAPI.GetDirContent(new DirContentRequest(userInfoResponse.Data.Value.BaseFolderUUID));
            foreach (FolderData folder in dirContentResponse.Data.Value.Folders) {

                // Decrypt the folder's metadata with the master keys
                FolderMetadata metadata = JsonSerializer.Deserialize<FolderMetadata>(FilenEncryption.DecryptMetadata(folder.EncryptedMetadata, filenClient.MasterKeys));
                Console.WriteLine($"- {folder.UUID}: {metadata.Name}");

            }

        }

    }

}