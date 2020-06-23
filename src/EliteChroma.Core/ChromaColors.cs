using System.Collections.Generic;
using Colore.Data;
using EliteChroma.Chroma;

namespace EliteChroma.Core
{
    public sealed class ChromaColors
    {
        private readonly Color[] _pips = new Color[9];

        internal ChromaColors()
        {
            BuildPipsColors();
        }

        public double DimBrightness { get; } = 0.04;

        public double SecondaryBindingBrightness { get; } = 0.2;

        public Color HardpointsToggle { get; } = Color.Red;

        public Color CargoScoopDeployed { get; } = Color.Orange;

        public Color CargoScoopRetracted { get; } = Color.Blue;

        public Color InterfaceMode { get; } = Color.White;

        public Color VehicleRotation { get; } = Color.White;

        public Color VehicleThrust { get; } = Color.Orange;

        public Color VehicleAlternate { get; } = Color.White;

        public Color VehicleThrottle { get; } = Color.Yellow;

        public Color VehicleMiscellaneous { get; } = Color.Pink;

        public Color VehicleTargeting { get; } = Color.Green;

        public Color VehicleWeapons { get; } = Color.Red;

        public Color VehicleCooling { get; } = Color.HotPink;

        public Color VehicleModeSwitches { get; } = Color.Green;

        public Color LandingGearDeployed { get; } = Color.Orange;

        public Color LandingGearRetracted { get; } = Color.Blue;

        public Color VehicleLightsOff { get; } = Color.Blue;

        public Color VehicleLightsMidBeam { get; } = new Color(0.5, 0.5, 1.0);

        public Color VehicleLightsHighBeam { get; } = Color.White;

        public Color Miscellaneous { get; } = Color.Blue;

        public Color FullSpectrumSystemScanner { get; } = Color.Blue;

        public Color FssCamera { get; } = Color.Green;

        public Color FssZoom { get; } = Color.Blue;

        public Color FssTuning { get; } = Color.Purple;

        public Color FssTarget { get; } = Color.Yellow;

        public Color FrameShiftDriveControls { get; } = Color.Pink;

        public Color PowerDistributor0 { get; } = Color.Red;

        public Color PowerDistributor100 { get; } = Color.Yellow;

        public Color PowerDistributorReset { get; } = new Color(0.5, 0.5, 0.5);

        public IReadOnlyList<Color> PowerDistributorScale => _pips;

        private void BuildPipsColors()
        {
            for (var i = 0; i < _pips.Length; i++)
            {
                _pips[i] = PowerDistributor0.Combine(PowerDistributor100, (double)i / (_pips.Length - 1));
            }
        }
    }
}
