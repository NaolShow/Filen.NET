namespace Filen {

    /// <summary>
    /// Represents credentials type that are required and might be asked through the authentication and login process
    /// </summary>
    public enum FilenCredential {

        /// <summary>
        /// Represents the user's email address
        /// </summary>
        Email,
        /// <summary>
        /// Represents the user's password
        /// </summary>
        Password,
        /// <summary>
        /// Represents the user's two factor authentication code
        /// </summary>
        Code

    }

}
