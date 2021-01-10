using System.Collections.Generic;
using System.Diagnostics;
using Colore.Data;
using EliteChroma.Chroma;

namespace EliteChroma.Core
{
    public sealed class ChromaColors
    {
        private const int _maxPips = 8;

        private readonly Color[] _pips = new Color[_maxPips + 1];

        private double _keyboardDimBrightness = 0.04;
        private double _deviceDimBrightness = 0.5;
        private double _secondaryBindingBrightness = 0.2;

        public ChromaColors()
        {
            _pips[0] = Color.Red;
            _pips[_maxPips] = Color.Yellow;
            BuildPipsColors();
        }

        public double KeyboardDimBrightness
        {
            get => _keyboardDimBrightness;
            set => _keyboardDimBrightness = Clamp(value, 0, 1, _keyboardDimBrightness);
        }

        public double DeviceDimBrightness
        {
            get => _deviceDimBrightness;
            set => _deviceDimBrightness = Clamp(value, 0, 1, _deviceDimBrightness);
        }

        public double SecondaryBindingBrightness
        {
            get => _secondaryBindingBrightness;
            set => _secondaryBindingBrightness = Clamp(value, 0, 1, _secondaryBindingBrightness);
        }

        public Color HardpointsToggle { get; set; } = Color.Red;

        public Color CargoScoopDeployed { get; set; } = Color.Orange;

        public Color CargoScoopRetracted { get; set; } = Color.Blue;

        public Color InterfaceMode { get; set; } = Color.White;

        public Color VehicleRotation { get; set; } = Color.White;

        public Color VehicleThrust { get; set; } = Color.Orange;

        public Color VehicleAlternate { get; set; } = Color.White;

        public Color VehicleThrottle { get; set; } = Color.Yellow;

        public Color VehicleMiscellaneous { get; set; } = Color.Pink;

        public Color VehicleTargeting { get; set; } = Color.Green;

        public Color VehicleWeapons { get; set; } = Color.Red;

        public Color VehicleCooling { get; set; } = Color.HotPink;

        public Color VehicleModeSwitches { get; set; } = Color.Green;

        public Color LandingGearDeployed { get; set; } = Color.Orange;

        public Color LandingGearRetracted { get; set; } = Color.Blue;

        public Color VehicleLightsOff { get; set; } = Color.Blue;

        public Color VehicleLightsMidBeam { get; set; } = new Color(0.5, 0.5, 1.0);

        public Color VehicleLightsHighBeam { get; set; } = Color.White;

        public Color Miscellaneous { get; set; } = Color.Blue;

        public Color FullSpectrumSystemScanner { get; set; } = Color.Blue;

        public Color FssCamera { get; set; } = Color.Green;

        public Color FssZoom { get; set; } = Color.Blue;

        public Color FssTuning { get; set; } = Color.Purple;

        public Color FssTarget { get; set; } = Color.Yellow;

        public Color FrameShiftDriveControls { get; set; } = Color.Pink;

        public Color PowerDistributor0
        {
            get => _pips[0];
            set
            {
                _pips[0] = value;
                BuildPipsColors();
            }
        }

        public Color PowerDistributor100
        {
            get => _pips[_maxPips];
            set
            {
                _pips[_maxPips] = value;
                BuildPipsColors();
            }
        }

        public Color PowerDistributorReset { get; set; } = new Color(0.5, 0.5, 0.5);

        public IReadOnlyList<Color> PowerDistributorScale => _pips;

        private static double Clamp(double value, double min, double max, double nanFallback)
        {
            Debug.Assert(min <= max, "Min cannot be greater than Max.");

            if (double.IsNaN(value))
            {
                return nanFallback;
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        private void BuildPipsColors()
        {
            for (var i = 1; i < _maxPips; i++)
            {
                _pips[i] = PowerDistributor0.Combine(PowerDistributor100, (double)i / _maxPips);
            }
        }
    }
}
