using Filen.API;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Filen {

    public partial class FilenClient {

        /// <summary>
        /// Called when <see cref="FilenClient"/> requires some credentials when using the <see cref="Login"/> method
        /// </summary>
        public event Func<FilenCredential, string>? OnRequireCredential = null;

        /// <summary>
        /// Determines if we are currently logged into <see href="https://filen.io"/>
        /// </summary>
        [MemberNotNullWhen(true, nameof(MasterKeys))]
        public bool IsLoggedIn => sessionData != null;

        private string? sessionPath;
        private SessionData? sessionData;

        /// <summary>
        /// Represents the user's master keys that can be used to decrypt metadata (available after <see cref="Login"/>)
        /// </summary>
        public IReadOnlyList<string>? MasterKeys => masterKeys;
        private string[]? masterKeys;

        /// <summary>
        /// Logins to <see href="https://filen.io"/> by asking credentials with the <see cref="OnRequireCredential"/> or using the <see cref="SessionData"/>
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the email, password or two factor code is null (from <see cref="OnRequireCredential"/>)</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when https://filen.io refuses to let us login for an unknown reason</exception>
        public async Task Login() {

            // If the session is available
            if (sessionPath != null && File.Exists(sessionPath)) {

                // Try to deserialize the session's json
                try {

                    // Deserialize the session data and set the underlying API key
                    // TODO: Maybe add an abstraction layer to let people do whatever they want for the session storage
                    sessionData = JsonSerializer.Deserialize<SessionData>(File.ReadAllText(sessionPath));
                    FilenAPI.SetApiKey(sessionData.Value.ApiKey);

                    // If the master keys has been fetched (mean that we have the right API key)
                    UserMasterKeysResponse masterKeysResponse = await FilenAPI.GetMasterKeys();
                    if (masterKeysResponse.Status) {

                        // Try to decrypt the master keys (if not possible then we have the wrong master key so we reconnect)
                        masterKeys = FilenEncryption.DecryptMasterKeys(masterKeysResponse.Data.Value.EncryptedMasterKeys, sessionData.Value.MasterKey);
                        if (masterKeys != null) return;

                    }

                    // If the session contained incorrect data (or the password has changed for example) then delete the file
                    File.Delete(sessionPath);

                } catch (JsonException) { }

            }

            // Get user's email and password
            string? email = OnRequireCredential?.Invoke(FilenCredential.Email);
            string? password = OnRequireCredential?.Invoke(FilenCredential.Password);
            if (email == null || password == null) throw new ArgumentNullException(email == null ? nameof(email) : nameof(password));

            // Get the auth info data and derive the password according to the salt
            // => This call should never fail
            AuthInfoResponse authInfoResponse = await FilenAPI.GetAuthInfo(new AuthInfoRequest(email));
            DerivedKeys derivedKeys = DerivedKeys.Derive(password, authInfoResponse.Data.Value);

            // Keep trying to login as long as the 2FA code is wrong (so start at the beginning with no two factor code)
            LoginResponse loginResponse = await FilenAPI.Login(new LoginRequest(authInfoResponse.Data.Value, derivedKeys));
            while (!loginResponse.Status && (loginResponse.Code == "invalid_params" || loginResponse.Code == "enter_2fa" || loginResponse.Code == "wrong_2fa"))
                loginResponse = await FilenAPI.Login(new LoginRequest(authInfoResponse.Data.Value, derivedKeys, OnRequireCredential?.Invoke(FilenCredential.Code)));

            // If the login didn't work for some reason
            if (!loginResponse.Status)
                throw new UnauthorizedAccessException(loginResponse.Code);

            // Save both the API key and the master key (unencrypted) to directly use the API instead of loggin-in
            // TODO: Might want to provide a way to tell a passphrase while login to not store this clearly on the disk
            sessionData = new SessionData() {
                ApiKey = loginResponse.Data.Value.ApiKey,
                MasterKey = derivedKeys.MasterKey
            };

            // Decrypt the master keys (if the login was successful we must have them, because we have the right key)
            masterKeys = FilenEncryption.DecryptMasterKeys(loginResponse.Data.Value.EncryptedMasterKeys, sessionData.Value.MasterKey);

            // If we should store the session data then store it
            if (sessionPath != null) File.WriteAllText(sessionPath, JsonSerializer.Serialize(sessionData, new JsonSerializerOptions() {
                WriteIndented = true
            }));

        }

    }

}
