using EliteFiles.Status.Internal;
using Newtonsoft.Json;

namespace EliteFiles.Status
{
    /// <summary>
    /// Represents the ship's power distributor status.
    /// </summary>
    [JsonConverter(typeof(PowerDistributorConverter))]
    public sealed class PowerDistributor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerDistributor"/> class.
        /// </summary>
        /// <param name="sys">The SYS capacitor level in half-pips (i.e. 0-8).</param>
        /// <param name="eng">The ENG capacitor level in half-pips (i.e. 0-8).</param>
        /// <param name="wep">The WEP capacitor level in half-pips (i.e. 0-8).</param>
        public PowerDistributor(byte sys, byte eng, byte wep)
        {
            Sys = sys;
            Eng = eng;
            Wep = wep;
        }

        /// <summary>
        /// Gets the SYS capacitor level in half-pips (i.e. 0-8).
        /// </summary>
        public byte Sys { get; }

        /// <summary>
        /// Gets the ENG capacitor level in half-pips (i.e. 0-8).
        /// </summary>
        public byte Eng { get; }

        /// <summary>
        /// Gets the WEP capacitor level in half-pips (i.e. 0-8).
        /// </summary>
        public byte Wep { get; }
    }
}
