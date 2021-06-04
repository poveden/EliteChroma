using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Design;
using Colore.Data;
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
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color HardpointsToggle { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color CargoScoopDeployed { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color CargoScoopRetracted { get; set; }

        [Category(_hudCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color InterfaceMode { get; set; }

        [Category(_thrustAttitudeCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleRotation { get; set; }

        [Category(_thrustAttitudeCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleThrust { get; set; }

        [Category(_thrustAttitudeCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleAlternate { get; set; }

        [Category(_thrustAttitudeCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleThrottle { get; set; }

        [Category(_thrustAttitudeCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleMiscellaneous { get; set; }

        [Category(_weaponsTargetingCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleTargeting { get; set; }

        [Category(_weaponsTargetingCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleWeapons { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleCooling { get; set; }

        [Category(_hudCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleModeSwitches { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color LandingGearDeployed { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color LandingGearRetracted { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleLightsOff { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleLightsMidBeam { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color VehicleLightsHighBeam { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color Miscellaneous { get; set; }

        [Category(_fssCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color FullSpectrumSystemScanner { get; set; }

        [Category(_fssCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color FssCamera { get; set; }

        [Category(_fssCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color FssZoom { get; set; }

        [Category(_fssCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color FssTuning { get; set; }

        [Category(_fssCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color FssTarget { get; set; }

        [Category(_subsystemsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color FrameShiftDriveControls { get; set; }

        [Category(_powerDistributorCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color PowerDistributor0 { get; set; }

        [Category(_powerDistributorCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color PowerDistributor100 { get; set; }

        [Category(_powerDistributorCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color PowerDistributorReset { get; set; }

        [Category(_ambientColorsCategory)]
        [TypeConverter(typeof(ColoreColorConverter))]
        [Editor(typeof(ColoreColorEditor), typeof(UITypeEditor))]
        public Color LandingMode { get; set; }

        [Browsable(false)]
        public IReadOnlyList<Color> PowerDistributorScale { get; }

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
