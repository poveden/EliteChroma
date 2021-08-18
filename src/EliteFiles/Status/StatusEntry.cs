﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace EliteFiles.Status
{
    /// <summary>
    /// Represents an Elite:Dangerous game status entry.
    /// </summary>
    /// <remarks>
    /// Reference: <a href="https://hosting.zaonce.net/community/journal/v31/Journal_Manual_v31.pdf">Elite:Dangerous Player Journal</a>.
    /// </remarks>
    public sealed class StatusEntry
    {
        [JsonExtensionData]
        private readonly Dictionary<string, JToken> _additionalFields = new Dictionary<string, JToken>(StringComparer.Ordinal);

        /// <summary>
        /// Gets or sets the timestamp of the event.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the type of event.
        /// </summary>
        [JsonProperty("event")]
        public string? Event { get; set; } = "Status";

        /// <summary>
        /// Gets or sets the game status flags.
        /// </summary>
        [JsonProperty("Flags")]
        public Flags Flags { get; set; }

        /// <summary>
        /// Gets or sets the game status flags.
        /// </summary>
        [JsonProperty("Flags2")]
        public Flags2 Flags2 { get; set; }

        /// <summary>
        /// Gets or sets the oxygen level when on foot.
        /// </summary>
        [JsonProperty("Oxygen")]
        public double? Oxygen { get; set; }

        /// <summary>
        /// Gets or sets the health level when on foot.
        /// </summary>
        [JsonProperty("Health")]
        public double? Health { get; set; }

        /// <summary>
        /// Gets or sets the ambient temperature when on foot.
        /// </summary>
        [JsonProperty("Temperature")]
        public double? Temperature { get; set; }

        /// <summary>
        /// Gets or sets the selected weapon when on foot.
        /// </summary>
        [JsonProperty("SelectedWeapon")]
        public string? SelectedWeapon { get; set; }

        /// <summary>
        /// Gets or sets the gravity amount when on foot.
        /// </summary>
        [JsonProperty("Gravity")]
        public double? Gravity { get; set; }

        /// <summary>
        /// Gets or sets the current ship's power distributor status.
        /// </summary>
        [JsonProperty("Pips")]
        public PowerDistributor? Pips { get; set; }

        /// <summary>
        /// Gets or sets the currently selected firegroup number.
        /// </summary>
        [JsonProperty("FireGroup")]
        public byte? FireGroup { get; set; }

        /// <summary>
        /// Gets or sets the currently selected GUI screen.
        /// </summary>
        [JsonProperty("GuiFocus")]
        public GuiFocus? GuiFocus { get; set; }

        /// <summary>
        /// Gets or sets the current ship's fuel levels.
        /// </summary>
        [JsonProperty("Fuel")]
        public Fuel? Fuel { get; set; }

        /// <summary>
        /// Gets or sets the current cargo load in tons.
        /// </summary>
        [JsonProperty("Cargo")]
        public double? Cargo { get; set; }

        /// <summary>
        /// Gets or sets the pilot's legal state.
        /// </summary>
        [JsonProperty("LegalState")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LegalState? LegalState { get; set; }

        /// <summary>
        /// Gets or sets the ship's latitude.
        /// </summary>
        [JsonProperty("Latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the ship's longitude.
        /// </summary>
        [JsonProperty("Longitude")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the ship's heading.
        /// </summary>
        [JsonProperty("Heading")]
        public double? Heading { get; set; }

        /// <summary>
        /// Gets or sets the ship's altitude.
        /// </summary>
        [JsonProperty("Altitude")]
        public double? Altitude { get; set; }

        /// <summary>
        /// Gets or sets the body name.
        /// </summary>
        [JsonProperty("BodyName")]
        public string? BodyName { get; set; }

        /// <summary>
        /// Gets or sets the planet radius.
        /// </summary>
        [JsonProperty("PlanetRadius")]
        public double? PlanetRadius { get; set; }

        /// <summary>
        /// Gets a collection of additional fields that may be included in the status.
        /// </summary>
        public IDictionary<string, JToken> AdditionalFields => _additionalFields;

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

            using var sr = new StreamReader(fs);

            var serializer = new JsonSerializer();
            return (StatusEntry?)serializer.Deserialize(sr, typeof(StatusEntry));
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
