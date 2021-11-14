using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Design;
using ChromaWrapper;
using EliteChroma.Core;

namespace EliteChroma.Internal.UI
{
    internal sealed class ChromaColorsMetadata
    {
        private const string _resourceNamePrefix = "Colors_";
        private const string _categoryPrefix = _resourceNamePrefix + "Category_";

        private const string _brightnessCategory = _categoryPrefix + "Brightness";
        private const string _fssCategory = _categoryPrefix + "FSS";
        private const string _hudCategory = _categoryPrefix + "HUD";
        private const string _powerDistributorCategory = _categoryPrefix + "PowerDistributor";
        private const string _subsystemsCategory = _categoryPrefix + "Subsystems";
        private const string _thrustAttitudeCategory = _categoryPrefix + "ThrustAttitude";
        private const string _weaponsTargetingCategory = _categoryPrefix + "WeaponsTargeting";
        private const string _ambientColorsCategory = _categoryPrefix + "AmbientColors";
        private const string _onFootCategory = _categoryPrefix + "OnFoot";

        [Category(_brightnessCategory)]
        [TypeConverter(typeof(BrightnessConverter))]
        [Editor(typeof(BrightnessEditor), typeof(UITypeEditor))]
        public double KeyboardDimBrightness { get; set; }

        [Category(_brightnessCategory)]
        [TypeConverter(typeof(BrightnessConverter))]
        [Editor(typeof(BrightnessEditor), typeof(UITypeEditor))]
        public double DeviceDimBrightness { get; set; }

        [Category(_brightnessCategory)]
        [TypeConverter(typeof(BrightnessConverter))]
        [Editor(typeof(BrightnessEditor), typeof(UITypeEditor))]
        public double SecondaryBindingBrightness { get; set; }

        [Category(_weaponsTargetingCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor HardpointsToggle { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor CargoScoopDeployed { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor CargoScoopRetracted { get; set; }

        [Category(_hudCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor InterfaceMode { get; set; }

        [Category(_thrustAttitudeCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleRotation { get; set; }

        [Category(_thrustAttitudeCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleThrust { get; set; }

        [Category(_thrustAttitudeCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleAlternate { get; set; }

        [Category(_thrustAttitudeCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleThrottle { get; set; }

        [Category(_thrustAttitudeCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleMiscellaneous { get; set; }

        [Category(_weaponsTargetingCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleTargeting { get; set; }

        [Category(_weaponsTargetingCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleWeapons { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleCooling { get; set; }

        [Category(_hudCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleModeSwitches { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor LandingGearDeployed { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor LandingGearRetracted { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleLightsOff { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleLightsMidBeam { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor VehicleLightsHighBeam { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor Miscellaneous { get; set; }

        [Category(_fssCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor FullSpectrumSystemScanner { get; set; }

        [Category(_fssCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor FssCamera { get; set; }

        [Category(_fssCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor FssZoom { get; set; }

        [Category(_fssCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor FssTuning { get; set; }

        [Category(_fssCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor FssTarget { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor FrameShiftDriveControls { get; set; }

        [Category(_powerDistributorCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor PowerDistributor0 { get; set; }

        [Category(_powerDistributorCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor PowerDistributor100 { get; set; }

        [Category(_powerDistributorCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor PowerDistributorReset { get; set; }

        [Category(_ambientColorsCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor LandingMode { get; set; }

        [Category(_onFootCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor OnFootHeadlook { get; set; }

        [Category(_onFootCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor OnFootMovement { get; set; }

        [Category(_onFootCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor OnFootModeSwitches { get; set; }

        [Category(_onFootCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor OnFootLightsToggle { get; set; }

        [Category(_onFootCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor OnFootShieldsToggle { get; set; }

        [Category(_onFootCategory)]
        [TypeConverter(typeof(ChromaColorConverter))]
        [Editor(typeof(ChromaColorEditor), typeof(UITypeEditor))]
        public ChromaColor OnFootInteract { get; set; }

        [Browsable(false)]
        public IReadOnlyList<ChromaColor>? PowerDistributorScale { get; }

        public static void InitTypeDescriptionProvider()
        {
            TypeDescriptor.AddProvider(new DescriptionProvider(), typeof(ChromaColors));
        }

        private sealed class DescriptionProvider : AssociatedMetadataTypeTypeDescriptionProvider
        {
            public DescriptionProvider()
                : base(typeof(ChromaColors), typeof(ChromaColorsMetadata))
            {
            }

            public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
            {
                ICustomTypeDescriptor baseDescriptor = base.GetTypeDescriptor(objectType, instance);
                return new LocalizedTypeDescriptor(baseDescriptor, _resourceNamePrefix);
            }
        }
    }
}
