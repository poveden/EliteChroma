using System.Text.Json.Serialization;

namespace EliteFiles.Status
{
    /// <summary>
    /// Represents the ship's fuel levels.
    /// </summary>
    public sealed class Fuel
    {
        /// <summary>
        /// Gets or sets the main fuel level in tons.
        /// </summary>
        [JsonPropertyName("FuelMain")]
        public double FuelMain { get; set; }

        /// <summary>
        /// Gets or sets the reservoir fuel level in tons.
        /// </summary>
        [JsonPropertyName("FuelReservoir")]
        public double FuelReservoir { get; set; }
    }
}
