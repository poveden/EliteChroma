<div align="center">
  <img src="img/EliteChroma.png" alt="EliteChroma logo" />
</div>

# EliteChroma

A tool to make [Razer Chroma](https://www.razer.com/chroma) devices react to [Elite:Dangerous](https://www.elitedangerous.com/) in-game events.

If a picture is worth a thousand words, then perhaps a video will be better still:

<div align="center">
  <figure>
    <div><a href="https://www.youtube.com/watch?v=2y30BFb3810"><img src="https://img.youtube.com/vi/2y30BFb3810/0.jpg" alt="EliteChroma short demo on YouTube" /></a></div>
    <figcaption>EliteChroma short demo on YouTube</figcaption>
  </figure>
</div>

## Features

- Runs in the background, accessible through a system tray notification icon.
- Retrieves device key bindings from the game configuration (currently limited to keyboard devices)
- Retrieves the current HUD color from the game configuration (currently used only as device background color)
- Watches for changes in the [game journal files](http://edcodex.info/?m=doc) and highlights the appropriate keys
- Performs color animation on certain events:
  - Supercruise/hyperspace jumps
  - Landing gear deployed
  - Cargo scoop deployed
  - Hyperspace jump destination hazard level indicator

## Application requirements

- [Microsoft .NET Core Desktop Runtime 3.1](https://dotnet.microsoft.com/download/dotnet-core/current/runtime) (_x64_ version)
- [Razer Synapse 3](https://www.razer.com/synapse-3) or the [Razer Chroma SDK](https://developer.razer.com/works-with-chroma/download/)
- A Razer Chroma keyboard (tested with Razer Cynosa Chroma)

## How to use

Download and run the MSI installer from the [Releases](https://github.com/poveden/EliteChroma/releases) page.

Once installed, just run EliteChroma from the start menu. You may then control the program from the icon in the system tray.

## Application development requirements

- [Visual Studio Community 2019](https://visualstudio.microsoft.com/vs/) with the _.NET desktop development workload_ installed
- [Razer Synapse 3](https://www.razer.com/synapse-3) or the [Razer Chroma SDK](https://developer.razer.com/works-with-chroma/download/)
- A Razer Chroma device

## Contributing

Contributions are welcome! Please read on [how to contribute](https://github.com/poveden/EliteChroma/blob/master/CONTRIBUTING.md).

For non-developer stuff, a [Frontier Forums thread](https://forums.frontier.co.uk/threads/elitechroma.534200/) is available so you can leave any comments or suggestions.

## Code of conduct

We follow the [Contributor Covenant Code of Conduct](https://github.com/poveden/EliteChroma/blob/master/CODE_OF_CONDUCT.md).

## Attributions

- [Frontier Developments](https://www.frontier.co.uk/), creators of such a wonderful game.
- [Colore .NET library](https://github.com/chroma-sdk/Colore), a powerful and elegant C# library for Razer Chroma's SDK.
- Heavy inspiration from the [EliteAPI .NET library](https://github.com/EliteAPI/EliteAPI).
- [WiX Toolset](https://wixtoolset.org/) used for creating Windows Installer packages.
