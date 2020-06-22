using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class FssLayer : LayerBase
    {
        private static readonly IReadOnlyCollection<string> FssCamera = new[]
        {
            FullSpectrumSystemScanner.CameraPitch,
            FullSpectrumSystemScanner.CameraPitchIncrease,
            FullSpectrumSystemScanner.CameraPitchDecrease,
            FullSpectrumSystemScanner.CameraYaw,
            FullSpectrumSystemScanner.CameraYawIncrease,
            FullSpectrumSystemScanner.CameraYawDecrease,
        };

        private static readonly IReadOnlyCollection<string> FssZoom = new[]
        {
            FullSpectrumSystemScanner.ZoomIn,
            FullSpectrumSystemScanner.ZoomOut,
            FullSpectrumSystemScanner.MiniZoomIn,
            FullSpectrumSystemScanner.MiniZoomOut,
        };

        private static readonly IReadOnlyCollection<string> FssTuning = new[]
        {
            FullSpectrumSystemScanner.RadioTuningXIncrease,
            FullSpectrumSystemScanner.RadioTuningXDecrease,
        };

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.Status.GuiFocus != GuiFocus.FssMode)
            {
                return;
            }

            var k = canvas.Keyboard;
            k[Key.Escape] = Color.White;

            ApplyColorToBinding(canvas.Keyboard, FssCamera, Color.Green);
            ApplyColorToBinding(canvas.Keyboard, FssZoom, Color.Blue);
            ApplyColorToBinding(canvas.Keyboard, FssTuning, Color.Purple);
            ApplyColorToBinding(canvas.Keyboard, FullSpectrumSystemScanner.DiscoveryScan, Game.Colors.AnalysisMode);
            ApplyColorToBinding(canvas.Keyboard, FullSpectrumSystemScanner.Quit, Color.White);
            ApplyColorToBinding(canvas.Keyboard, FullSpectrumSystemScanner.Target, Color.Yellow);
            ApplyColorToBinding(canvas.Keyboard, FullSpectrumSystemScanner.ShowHelp, Game.Colors.Hud);
        }
    }
}
