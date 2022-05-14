using System.Diagnostics;
using System.Drawing;
using ChromaWrapper;
using EliteChroma.Chroma;

namespace EliteChroma.Core
{
    public sealed class ChromaColors
    {
        private const int _maxPips = 8;

        private readonly ChromaColor[] _pips = new ChromaColor[_maxPips + 1];

        private double _keyboardDimBrightness = 0.04;
        private double _deviceDimBrightness = 0.5;
        private double _secondaryBindingBrightness = 0.2;

        public ChromaColors()
        {
            _pips[0] = ChromaColor.Red;
            _pips[_maxPips] = ChromaColor.Yellow;
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

        public ChromaColor HardpointsToggle { get; set; } = ChromaColor.Red;

        public ChromaColor CargoScoopDeployed { get; set; } = ChromaColor.FromColor(Color.Orange);

        public ChromaColor CargoScoopRetracted { get; set; } = ChromaColor.Blue;

        public ChromaColor InterfaceMode { get; set; } = ChromaColor.White;

        public ChromaColor VehicleRotation { get; set; } = ChromaColor.White;

        public ChromaColor VehicleThrust { get; set; } = ChromaColor.FromColor(Color.Orange);

        public ChromaColor VehicleAlternate { get; set; } = ChromaColor.White;

        public ChromaColor VehicleThrottle { get; set; } = ChromaColor.Yellow;

        public ChromaColor VehicleMiscellaneous { get; set; } = ChromaColor.Magenta;

        public ChromaColor VehicleTargeting { get; set; } = ChromaColor.Green;

        public ChromaColor VehicleWeapons { get; set; } = ChromaColor.Red;

        public ChromaColor VehicleCooling { get; set; } = ChromaColor.FromColor(Color.HotPink);

        public ChromaColor VehicleModeSwitches { get; set; } = ChromaColor.Green;

        public ChromaColor LandingGearDeployed { get; set; } = ChromaColor.FromColor(Color.Orange);

        public ChromaColor LandingGearRetracted { get; set; } = ChromaColor.Blue;

        public ChromaColor VehicleLightsOff { get; set; } = ChromaColor.Blue;

        public ChromaColor VehicleLightsMidBeam { get; set; } = ChromaColor.FromRgb(0.5, 0.5, 1.0);

        public ChromaColor VehicleLightsHighBeam { get; set; } = ChromaColor.White;

        public ChromaColor Miscellaneous { get; set; } = ChromaColor.Blue;

        public ChromaColor FullSpectrumSystemScanner { get; set; } = ChromaColor.Blue;

        public ChromaColor FssCamera { get; set; } = ChromaColor.Green;

        public ChromaColor FssZoom { get; set; } = ChromaColor.Blue;

        public ChromaColor FssTuning { get; set; } = ChromaColor.FromColor(Color.Purple);

        public ChromaColor FssTarget { get; set; } = ChromaColor.Yellow;

        public ChromaColor FrameShiftDriveControls { get; set; } = ChromaColor.Magenta;

        public ChromaColor PowerDistributor0
        {
            get => _pips[0];
            set
            {
                _pips[0] = value;
                BuildPipsColors();
            }
        }

        public ChromaColor PowerDistributor100
        {
            get => _pips[_maxPips];
            set
            {
                _pips[_maxPips] = value;
                BuildPipsColors();
            }
        }

        public ChromaColor PowerDistributorReset { get; set; } = ChromaColor.FromRgb(0.5, 0.5, 0.5);

        public ChromaColor LandingMode { get; set; } = ChromaColor.Blue;

        public ChromaColor OnFootHeadlook { get; set; } = ChromaColor.White;

        public ChromaColor OnFootMovement { get; set; } = ChromaColor.FromColor(Color.Orange);

        public ChromaColor OnFootModeSwitches { get; set; } = ChromaColor.Green;

        public ChromaColor OnFootLightsToggle { get; set; } = ChromaColor.Blue;

        public ChromaColor OnFootShieldsToggle { get; set; } = ChromaColor.Cyan;

        public ChromaColor OnFootInteract { get; set; } = ChromaColor.FromColor(Color.Purple);

        public IReadOnlyList<ChromaColor> PowerDistributorScale => _pips;

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
            for (int i = 1; i < _maxPips; i++)
            {
                _pips[i] = PowerDistributor0.Combine(PowerDistributor100, (double)i / _maxPips);
            }
        }
    }
}
