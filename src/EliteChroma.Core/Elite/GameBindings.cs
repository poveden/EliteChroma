using System;
using System.Collections.Generic;
using System.Linq;
using EliteFiles.Bindings;
using EliteFiles.Bindings.Devices;

namespace EliteChroma.Elite
{
    public sealed class GameBindings
    {
        public const string Screenshot = "Hardcoded_Screenshot";
        public const string HiResScreenshot = "Hardcoded_HiResScreenshot";
        public const string ToggleHud = "Hardcoded_ToggleHUD";
        public const string ToggleFps = "Hardcoded_ToggleFPS";
        public const string ToggleBandwidth = "Hardcoded_ToggleBandwidth";

        private static readonly IReadOnlyDictionary<string, Binding> _hardcodedBindings = new Dictionary<string, Binding>(StringComparer.Ordinal)
        {
            [Screenshot] = new Binding(Screenshot, BuildKeyboardBinding(Keyboard.F10), null),
            [HiResScreenshot] = new Binding(HiResScreenshot, BuildKeyboardBinding(Keyboard.F10, Keyboard.LeftAlt), null),
            [ToggleHud] = new Binding(ToggleHud, BuildKeyboardBinding("Key_G", Keyboard.LeftControl, Keyboard.LeftAlt), null),
            [ToggleFps] = new Binding(ToggleFps, BuildKeyboardBinding("Key_F", Keyboard.LeftControl), null),
            [ToggleBandwidth] = new Binding(ToggleBandwidth, BuildKeyboardBinding("Key_B", Keyboard.LeftControl), null),
        };

        private readonly Dictionary<string, Binding> _bindings = new Dictionary<string, Binding>(StringComparer.Ordinal);
        private readonly HashSet<DeviceKey> _modifiers = new HashSet<DeviceKey>();

        internal GameBindings(BindingPreset bindingPreset)
        {
            if (bindingPreset == null)
            {
                throw new ArgumentNullException(nameof(bindingPreset));
            }

            KeyboardLayout = bindingPreset.KeyboardLayout;

            foreach ((string bindingName, Binding binding) in _hardcodedBindings.Concat(bindingPreset.Bindings))
            {
                _bindings[bindingName] = binding;

                foreach (DeviceKey modifier in binding.Primary.Modifiers)
                {
                    _ = _modifiers.Add(modifier);
                }

                foreach (DeviceKey modifier in binding.Secondary.Modifiers)
                {
                    _ = _modifiers.Add(modifier);
                }
            }
        }

        public string? KeyboardLayout { get; }

        public IEnumerable<DeviceKey> Modifiers => _modifiers;

        public bool TryGetValue(string bindingName, out Binding? binding)
        {
            return _bindings.TryGetValue(bindingName, out binding);
        }

        private static DeviceKeyCombination BuildKeyboardBinding(string key, params string[] modifiers)
        {
            IEnumerable<DeviceKey> dkModifiers = modifiers.Select(x => new DeviceKey("Keyboard", x));

            return new DeviceKeyCombination("Keyboard", key, dkModifiers);
        }
    }
}
