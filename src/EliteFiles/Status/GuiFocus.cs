namespace EliteFiles.Status
{
    /// <summary>
    /// Represents the selected GUI screen.
    /// </summary>
    public enum GuiFocus
    {
        /// <summary>No focus (i.e. main cockpit view).</summary>
        None = 0,

        /// <summary>Internal panel (i.e. right hand side).</summary>
        InternalPanel,

        /// <summary>External panel (i.e. left hand side).</summary>
        ExternalPanel,

        /// <summary>Comms panel.</summary>
        CommsPanel,

        /// <summary>Role panel.</summary>
        RolePanel,

        /// <summary>Station services.</summary>
        StationServices,

        /// <summary>Galaxy map.</summary>
        GalaxyMap,

        /// <summary>System map.</summary>
        SystemMap,

        /// <summary>Orrery.</summary>
        Orrery,

        /// <summary>FSS mode.</summary>
        FssMode,

        /// <summary>SAA mode.</summary>
        SaaMode,

        /// <summary>Codex.</summary>
        Codex,
    }
}
