using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names while on foot.
    /// </summary>
    public static class OnFoot
    {
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
    }
}
