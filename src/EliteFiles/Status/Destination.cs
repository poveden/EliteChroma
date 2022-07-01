using System.Text.Json.Serialization;

namespace EliteFiles.Status
{
    /// <summary>
    /// Represents the ship's selected navigation target.
    /// </summary>
    public sealed class Destination
    {
        /// <summary>
        /// Gets or sets the ID of the system where the destination is.
        /// </summary>
        [JsonPropertyName("System")]
        public long System { get; set; }

        /// <summary>
        /// Gets or sets index of the destination within the system.
        /// </summary>
        [JsonPropertyName("Body")]
        public int Body { get; set; }

        /// <summary>
        /// Gets or sets the name of the selected destination.
        /// </summary>
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the localized name of the selected destination.
        /// </summary>
        [JsonPropertyName("Name_Localised")]
        public string? NameLocalized { get; set; }
    }
}
