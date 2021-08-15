﻿using System.Diagnostics.CodeAnalysis;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class InterfaceModeLayer : LayerBase
    {
        public override int Order => 900;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.AtHelm && !Game.DockedOrLanded && Game.Status.GuiFocus == GuiFocus.None)
            {
                return;
            }

            CustomKeyboardEffect k = canvas.Keyboard;
            k[Key.Escape] = Colors.InterfaceMode;

            if (Game.IsWalking)
            {
                return;
            }

            if (Game.Status.HasFlag(Flags2.InTaxi))
            {
                ApplyColorToBinding(canvas.Keyboard, InterfaceMode.Select, Colors.InterfaceMode);
                return;
            }

            ApplyColorToBinding(canvas.Keyboard, InterfaceMode.All, Colors.InterfaceMode);
        }
    }
}
