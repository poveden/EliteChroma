using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using EliteFiles.Internal;

namespace EliteFiles.Status
{
    /// <summary>
    /// Represents an Elite:Dangerous game status entry.
    /// </summary>
    public sealed class StatusEntry
    {
        private readonly Dictionary<string, JsonElement> _additionalFields = new Dictionary<string, JsonElement>(StringComparer.Ordinal);

        /// <summary>
        /// Gets or sets the timestamp of the event.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the type of event.
        /// </summary>
        [JsonPropertyName("event")]
        public string? Event { get; set; } = "Status";

        /// <summary>
        /// Gets or sets the game status flags.
        /// </summary>
        [JsonPropertyName("Flags")]
        public Flags Flags { get; set; }

        /// <summary>
        /// Gets or sets the game status flags.
        /// </summary>
        [JsonPropertyName("Flags2")]
        public Flags2 Flags2 { get; set; }

        /// <summary>
        /// Gets or sets the oxygen level when on foot.
        /// </summary>
        [JsonPropertyName("Oxygen")]
        public double? Oxygen { get; set; }

        /// <summary>
        /// Gets or sets the health level when on foot.
        /// </summary>
        [JsonPropertyName("Health")]
        public double? Health { get; set; }

        /// <summary>
        /// Gets or sets the ambient temperature when on foot.
        /// </summary>
        [JsonPropertyName("Temperature")]
        public double? Temperature { get; set; }

        /// <summary>
        /// Gets or sets the selected weapon when on foot.
        /// </summary>
        [JsonPropertyName("SelectedWeapon")]
        public string? SelectedWeapon { get; set; }

        /// <summary>
        /// Gets or sets the gravity amount when on foot.
        /// </summary>
        [JsonPropertyName("Gravity")]
        public double? Gravity { get; set; }

        /// <summary>
        /// Gets or sets the current ship's power distributor status.
        /// </summary>
        [JsonPropertyName("Pips")]
        public PowerDistributor? Pips { get; set; }

        /// <summary>
        /// Gets or sets the currently selected firegroup number.
        /// </summary>
        [JsonPropertyName("FireGroup")]
        public byte? FireGroup { get; set; }

        /// <summary>
        /// Gets or sets the currently selected GUI screen.
        /// </summary>
        [JsonPropertyName("GuiFocus")]
        public GuiFocus? GuiFocus { get; set; }

        /// <summary>
        /// Gets or sets the current ship's fuel levels.
        /// </summary>
        [JsonPropertyName("Fuel")]
        public Fuel? Fuel { get; set; }

        /// <summary>
        /// Gets or sets the current cargo load in tons.
        /// </summary>
        [JsonPropertyName("Cargo")]
        public double? Cargo { get; set; }

        /// <summary>
        /// Gets or sets the pilot's legal state.
        /// </summary>
        [JsonPropertyName("LegalState")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LegalState LegalState { get; set; }

        /// <summary>
        /// Gets or sets the ship's latitude.
        /// </summary>
        [JsonPropertyName("Latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the ship's longitude.
        /// </summary>
        [JsonPropertyName("Longitude")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the ship's heading.
        /// </summary>
        [JsonPropertyName("Heading")]
        public double? Heading { get; set; }

        /// <summary>
        /// Gets or sets the ship's altitude.
        /// </summary>
        [JsonPropertyName("Altitude")]
        public double? Altitude { get; set; }

        /// <summary>
        /// Gets or sets the body name.
        /// </summary>
        [JsonPropertyName("BodyName")]
        public string? BodyName { get; set; }

        /// <summary>
        /// Gets or sets the planet radius.
        /// </summary>
        [JsonPropertyName("PlanetRadius")]
        public double? PlanetRadius { get; set; }

        /// <summary>
        /// Gets a collection of additional fields that may be included in the status.
        /// </summary>
        [JsonExtensionData]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "https://github.com/dotnet/runtime/issues/30258#issuecomment-604732779")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:Property summary documentation should match accessors", Justification = "https://github.com/dotnet/runtime/issues/30258#issuecomment-604732779")]
        public IDictionary<string, JsonElement> AdditionalFields
        {
            get => _additionalFields;

            // Reference: https://github.com/dotnet/runtime/issues/30258#issuecomment-604732779
            [ExcludeFromCodeCoverage]
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Reads the game status from the given file.
        /// </summary>
        /// <param name="path">The path to the game status file.</param>
        /// <returns>The status entry, or <c>null</c> if the file couldn't be read (e.g. in the middle of an update).</returns>
        public static StatusEntry? FromFile(string path)
        {
            using FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            if (fs.Length == 0)
            {
                return null;
            }

            return JsonSerializer.Deserialize(fs, EliteFilesSerializerContext.Default.StatusEntry);
        }

        /// <summary>
        /// Returns a value indicating wheter the given flag is currently set.
        /// </summary>
        /// <param name="flag">The flag to check.</param>
        /// <returns><c>true</c> if the flag is set; otherwise, <c>false</c>.</returns>
        public bool HasFlag(Flags flag)
        {
            return Flags.HasFlag(flag);
        }

        /// <summary>
        /// Returns a value indicating wheter the given flag is currently set.
        /// </summary>
        /// <param name="flag">The flag to check.</param>
        /// <returns><c>true</c> if the flag is set; otherwise, <c>false</c>.</returns>
        public bool HasFlag(Flags2 flag)
        {
            return Flags2.HasFlag(flag);
        }
    }
}
