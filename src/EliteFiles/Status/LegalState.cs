namespace EliteFiles.Status
{
    /// <summary>
    /// Represents the pilot's legal state.
    /// </summary>
    public enum LegalState
    {
        /// <summary>Unknown.</summary>
        Unknown = 0,

        /// <summary>Clean.</summary>
        Clean,

        /// <summary>Illegal cargo.</summary>
        IllegalCargo,

        /// <summary>Speeding.</summary>
        Speeding,

        /// <summary>Wanted.</summary>
        Wanted,

        /// <summary>Hostile.</summary>
        Hostile,

        /// <summary>Passenger wanted.</summary>
        PassengerWanted,

        /// <summary>Warrant.</summary>
        Warrant,

        /// <summary>Thargoid.</summary>
        Thargoid,

        /// <summary>Allied.</summary>
        Allied,
    }
}
