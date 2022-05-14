namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for flight orders.
    /// </summary>
    public static class FighterOrders
    {
        /// <summary>
        /// Gets the category of all <see cref="FighterOrders"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.ShipControls;

#pragma warning disable 1591, SA1600
        public const string RequestDock = "OrderRequestDock";
        public const string DefensiveBehaviour = "OrderDefensiveBehaviour";
        public const string AggressiveBehaviour = "OrderAggressiveBehaviour";
        public const string FocusTarget = "OrderFocusTarget";
        public const string HoldFire = "OrderHoldFire";
        public const string HoldPosition = "OrderHoldPosition";
        public const string Follow = "OrderFollow";
        public const string Open = "OpenOrders";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="FighterOrders"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(FighterOrders));
    }
}
