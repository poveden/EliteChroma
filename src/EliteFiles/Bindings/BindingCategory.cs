namespace EliteFiles.Bindings
{
    /// <summary>
    /// Specifies the category of a device binding.
    /// </summary>
    public enum BindingCategory
    {
        // IMPORTANT: This enum is treated as an indexer for entries in the "StartPreset.start" file,
        // so the first entry MUST be at index 0 and all entries MUST be consecutive.

        /// <summary>General controls category.</summary>
        /// <remarks>This is the only category available before Odyssey.</remarks>
        GeneralControls = 0,

        /// <summary>Ship controls category.</summary>
        ShipControls,

        /// <summary>SRV controls category.</summary>
        SrvControls,

        /// <summary>On foot controls category.</summary>
        OnFootControls,
    }
}
