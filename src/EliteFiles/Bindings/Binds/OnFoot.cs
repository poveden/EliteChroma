using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names while on foot.
    /// </summary>
    public static class OnFoot
    {
        /// <summary>
        /// Gets the category of all <see cref="OnFoot"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.OnFootControls;

#pragma warning disable 1591, SA1600
        public const string MouseXMode = "MouseHumanoidXMode";
        public const string MouseYMode = "MouseHumanoidYMode";
        public const string MouseSensitivity = "MouseHumanoidSensitivity";
        public const string ForwardAxis = "HumanoidForwardAxis";
        public const string ForwardButton = "HumanoidForwardButton";
        public const string BackwardButton = "HumanoidBackwardButton";
        public const string StrafeAxis = "HumanoidStrafeAxis";
        public const string StrafeLeftButton = "HumanoidStrafeLeftButton";
        public const string StrafeRightButton = "HumanoidStrafeRightButton";
        public const string RotateAxis = "HumanoidRotateAxis";
        public const string RotateSensitivity = "HumanoidRotateSensitivity";
        public const string RotateLeftButton = "HumanoidRotateLeftButton";
        public const string RotateRightButton = "HumanoidRotateRightButton";
        public const string PitchAxis = "HumanoidPitchAxis";
        public const string PitchSensitivity = "HumanoidPitchSensitivity";
        public const string PitchUpButton = "HumanoidPitchUpButton";
        public const string PitchDownButton = "HumanoidPitchDownButton";
        public const string SprintButton = "HumanoidSprintButton";
        public const string WalkButton = "HumanoidWalkButton";
        public const string CrouchButton = "HumanoidCrouchButton";
        public const string JumpButton = "HumanoidJumpButton";
        public const string PrimaryInteractButton = "HumanoidPrimaryInteractButton";
        public const string SecondaryInteractButton = "HumanoidSecondaryInteractButton";
        public const string ItemWheelButton = "HumanoidItemWheelButton";
        public const string ItemWheelButtonXAxis = "HumanoidItemWheelButton_XAxis";
        public const string ItemWheelButtonXLeft = "HumanoidItemWheelButton_XLeft";
        public const string ItemWheelButtonXRight = "HumanoidItemWheelButton_XRight";
        public const string ItemWheelButtonYAxis = "HumanoidItemWheelButton_YAxis";
        public const string ItemWheelButtonYUp = "HumanoidItemWheelButton_YUp";
        public const string ItemWheelButtonYDown = "HumanoidItemWheelButton_YDown";
        public const string ItemWheelAcceptMouseInput = "HumanoidItemWheel_AcceptMouseInput";
        public const string PrimaryFireButton = "HumanoidPrimaryFireButton";
        public const string ZoomButton = "HumanoidZoomButton";
        public const string ThrowGrenadeButton = "HumanoidThrowGrenadeButton";
        public const string MeleeButton = "HumanoidMeleeButton";
        public const string ReloadButton = "HumanoidReloadButton";
        public const string SwitchWeapon = "HumanoidSwitchWeapon";
        public const string SelectPrimaryWeaponButton = "HumanoidSelectPrimaryWeaponButton";
        public const string SelectSecondaryWeaponButton = "HumanoidSelectSecondaryWeaponButton";
        public const string SelectUtilityWeaponButton = "HumanoidSelectUtilityWeaponButton";
        public const string SelectNextWeaponButton = "HumanoidSelectNextWeaponButton";
        public const string SelectPreviousWeaponButton = "HumanoidSelectPreviousWeaponButton";
        public const string HideWeaponButton = "HumanoidHideWeaponButton";
        public const string SelectNextGrenadeTypeButton = "HumanoidSelectNextGrenadeTypeButton";
        public const string SelectPreviousGrenadeTypeButton = "HumanoidSelectPreviousGrenadeTypeButton";
        public const string ToggleFlashlightButton = "HumanoidToggleFlashlightButton";
        public const string ToggleNightVisionButton = "HumanoidToggleNightVisionButton";
        public const string ToggleShieldsButton = "HumanoidToggleShieldsButton";
        public const string SwitchToRechargeTool = "HumanoidSwitchToRechargeTool";
        public const string SwitchToCompAnalyser = "HumanoidSwitchToCompAnalyser";
        public const string SwitchToSuitTool = "HumanoidSwitchToSuitTool";
        public const string ToggleToolModeButton = "HumanoidToggleToolModeButton";
        public const string ToggleMissionHelpPanelButton = "HumanoidToggleMissionHelpPanelButton";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="OnFoot"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(OnFoot));

        /// <summary>
        /// Gets the collection of all headlook-related <see cref="OnFoot"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Headlook { get; } = new ReadOnlyCollection<string>(new[]
        {
            MouseXMode,
            MouseYMode,
            MouseSensitivity,
            RotateAxis,
            RotateSensitivity,
            RotateLeftButton,
            RotateRightButton,
            PitchAxis,
            PitchSensitivity,
            PitchUpButton,
            PitchDownButton,
        });

        /// <summary>
        /// Gets the collection of all movement-related <see cref="OnFoot"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Movement { get; } = new ReadOnlyCollection<string>(new[]
        {
            ForwardAxis,
            ForwardButton,
            BackwardButton,
            StrafeAxis,
            StrafeLeftButton,
            StrafeRightButton,
            SprintButton,
            WalkButton,
            CrouchButton,
            JumpButton,
        });

        /// <summary>
        /// Gets the collection of all interaction-related <see cref="OnFoot"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Interaction { get; } = new ReadOnlyCollection<string>(new[]
        {
            PrimaryInteractButton,
            SecondaryInteractButton,
        });

        /// <summary>
        /// Gets the collection of all item wheel-related <see cref="OnFoot"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> ItemWheel { get; } = new ReadOnlyCollection<string>(new[]
        {
            ItemWheelButton,
            ItemWheelButtonXAxis,
            ItemWheelButtonXLeft,
            ItemWheelButtonXRight,
            ItemWheelButtonYAxis,
            ItemWheelButtonYUp,
            ItemWheelButtonYDown,
            ItemWheelAcceptMouseInput,
        });

        /// <summary>
        /// Gets the collection of all weapons-related <see cref="OnFoot"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Weapons { get; } = new ReadOnlyCollection<string>(new[]
        {
            PrimaryFireButton,
            ZoomButton,
            ThrowGrenadeButton,
            MeleeButton,
            ReloadButton,
            SwitchWeapon,
            SelectPrimaryWeaponButton,
            SelectSecondaryWeaponButton,
            SelectNextWeaponButton,
            SelectPreviousWeaponButton,
            HideWeaponButton,
            SelectNextGrenadeTypeButton,
            SelectPreviousGrenadeTypeButton,
        });

        /// <summary>
        /// Gets the collection of all tools-related <see cref="OnFoot"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Tools { get; } = new ReadOnlyCollection<string>(new[]
        {
            SelectUtilityWeaponButton,
            SwitchToRechargeTool,
            SwitchToCompAnalyser,
            SwitchToSuitTool,
            ToggleToolModeButton,
        });

        /// <summary>
        /// Gets the collection of all suit-related <see cref="OnFoot"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Suit { get; } = new ReadOnlyCollection<string>(new[]
        {
            ToggleFlashlightButton,
            ToggleNightVisionButton,
            ToggleShieldsButton,
        });
    }
}
