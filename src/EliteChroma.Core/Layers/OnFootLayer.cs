using System.Diagnostics.CodeAnalysis;
using EliteChroma.Core.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class OnFootLayer : LayerBase
    {
        private static readonly IReadOnlyCollection<string> _movementSocialSpace = OnFoot.Movement.Except(new[]
        {
            OnFoot.JumpButton,
        }).ToList().AsReadOnly();

        private static readonly IReadOnlyCollection<string> _toolsExceptToggle = OnFoot.Tools.Except(new[]
        {
            OnFoot.ToggleToolModeButton,
        }).ToList().AsReadOnly();

        private static readonly IReadOnlyCollection<string> _weaponsExceptHide = OnFoot.Weapons.Except(new[]
        {
            OnFoot.HideWeaponButton,
        }).ToList().AsReadOnly();

        private static readonly IReadOnlyCollection<string> _modeSwitchesSocialSpace = OnFootModeSwitches.All.Except(new[]
        {
            OnFootModeSwitches.ConflictContextualUIButton,
        }).ToList().AsReadOnly();

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.IsWalking)
            {
                return;
            }

            ApplyColorToBinding(canvas.Keyboard, OnFoot.Headlook, Colors.OnFootHeadlook);
            ApplyColorToBinding(canvas.Keyboard, _movementSocialSpace, Colors.OnFootMovement);
            ApplyColorToBinding(canvas.Keyboard, OnFoot.PrimaryInteractButton, Colors.OnFootInteract);

            ApplyColorToBinding(canvas.Keyboard, OnFoot.ToggleFlashlightButton, Colors.OnFootLightsToggle);
            ApplyColorToBinding(canvas.Keyboard, _modeSwitchesSocialSpace, Colors.OnFootModeSwitches);

            if (!Game.Status.HasFlag(Flags2.OnFootExterior))
            {
                return;
            }

            ApplyColorToBinding(canvas.Keyboard, OnFoot.ItemWheel, Colors.OnFootModeSwitches);
            ApplyColorToBinding(canvas.Keyboard, OnFoot.JumpButton, Colors.OnFootMovement);
            ApplyColorToBinding(canvas.Keyboard, OnFoot.SecondaryInteractButton, Colors.OnFootInteract);
            ApplyColorToBinding(canvas.Keyboard, OnFoot.ToggleShieldsButton, Colors.OnFootShieldsToggle);
            ApplyColorToBinding(canvas.Keyboard, OnFoot.ToggleNightVisionButton, Colors.OnFootLightsToggle);
            ApplyColorToBinding(canvas.Keyboard, _weaponsExceptHide, Colors.HardpointsToggle);
            ApplyColorToBinding(canvas.Keyboard, _toolsExceptToggle, Game.Colors.AnalysisMode);
            ApplyColorToBinding(canvas.Keyboard, OnFootModeSwitches.ConflictContextualUIButton, Colors.OnFootModeSwitches);

            OnFootWeapon.Kind weaponKind = OnFootWeapon.GetKind(Game.Status.SelectedWeapon);

            switch (weaponKind)
            {
                case OnFootWeapon.Kind.Unknown:
                case OnFootWeapon.Kind.Unarmed:
                    break;

                case OnFootWeapon.Kind.Energylink:
                case OnFootWeapon.Kind.ProfileAnalyser:
                    ApplyColorToBinding(canvas.Keyboard, OnFoot.HideWeaponButton, Game.Colors.AnalysisMode);
                    ApplyColorToBinding(canvas.Keyboard, OnFoot.ToggleToolModeButton, Game.Colors.AnalysisMode);
                    break;

                case OnFootWeapon.Kind.GeneticSampler:
                case OnFootWeapon.Kind.ArcCutter:
                    ApplyColorToBinding(canvas.Keyboard, OnFoot.HideWeaponButton, Game.Colors.AnalysisMode);
                    break;

                case OnFootWeapon.Kind.Weapon:
                    ApplyColorToBinding(canvas.Keyboard, OnFoot.HideWeaponButton, Colors.HardpointsToggle);
                    break;

                default:
                    break;
            }
        }
    }
}
