using Newtonsoft.Json;

namespace EliteFiles.Status
{
    /// <summary>
    /// Represents the ship's fuel levels.
    /// </summary>
    public sealed class Fuel
    {
        /// <summary>
        /// Gets the main fuel level in tons.
        /// </summary>
        [JsonProperty("FuelMain")]
        public double FuelMain { get; private set; }

        /// <summary>
        /// Gets the reservoir fuel level in tons.
        /// </summary>
        [JsonProperty("FuelReservoir")]
        public double FuelReservoir { get; private set; }
    }
}
