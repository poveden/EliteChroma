using Newtonsoft.Json;

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
        [JsonProperty("FuelMain")]
        public double FuelMain { get; set; }

        /// <summary>
        /// Gets or sets the reservoir fuel level in tons.
        /// </summary>
        [JsonProperty("FuelReservoir")]
        public double FuelReservoir { get; set; }
    }
}
