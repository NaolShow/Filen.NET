using System.Text.Json.Serialization;

namespace Filen.API {

    /// <summary>
    /// Enumeration of every possible colors for the folders
    /// </summary>
    [JsonConverter(typeof(JsonLowerCaseEnumConverter<DirColor>))]
    public enum DirColor {

        /// <summary>
        /// Represents the "default" color which represents yellow
        /// </summary>
        Default,
        /// <summary>
        /// Represents the blue color
        /// </summary>
        Blue,
        /// <summary>
        /// Represents the green color
        /// </summary>
        Green,
        /// <summary>
        /// Represents the purple color
        /// </summary>
        Purple,
        /// <summary>
        /// Represents the red color
        /// </summary>
        Red,
        /// <summary>
        /// Represents the gray color
        /// </summary>
        Gray,

    }

}
