# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## [Unreleased](https://github.com/poveden/EliteChroma/compare/v1.4.0...HEAD)

### Added

- Add Chroma Link support (thanks [@mcargille](https://github.com/mcargille)!)
- Add mouse support

### Changed

- Improve hyperspace lighting
- Make hyperspace exit glare wash over existing colors

## [1.4.0](https://github.com/poveden/EliteChroma/compare/v1.3.0...v1.4.0) — 2020-04-05

### Changed

- Add game process tracking while in background
- Add Chroma SDK device access grant tracking
- Reduce strain on Razer devices

### Added

- Add installer
- Add Full System Spectrum (FSS) Scanner support
- Add jump destination system hazard level indicator
- Add star glare on hyperspace exit

## [1.3.0](https://github.com/poveden/EliteChroma/compare/v1.2.0...v1.3.0) — 2020-02-24

### Fixed

- Fix wrong game install paths returned for Steam Libraries
- Stop "hardpoints deployed" pulse during supercruise
- Fix game state concurrency issues during rendering

### Added

- Add Razer Chroma SDK check during startup

## [1.2.0](https://github.com/poveden/EliteChroma/compare/v1.1.1...v1.2.0) — 2020-02-18

### Fixed

- Add file reload retries to (hopefully) fix occassional skipping of game events
- `ChromaController` will now create a `NativeApi` instance only if no `IChromaApi` instance has been provided

### Added

- Add Steam Libraries to game install folder detection routine

## [1.1.1](https://github.com/poveden/EliteChroma/compare/v1.1.0...v1.1.1) — 2020-01-30

### Fixed

- Fix silent crash when the game install folder cannot be found
- Fix main process not exiting when `ValidateFolders()` returns `false`

## [1.1.0](https://github.com/poveden/EliteChroma/compare/v1.0.1...v1.1.0) — 2020-01-13

### Fixed

- Settings dialog box now validates correctly the selected game options folder
- Fix crash while switching bindings in game

### Added

- Add `ChromaAppInfo.xml`
- Add help links to game folder settings dialog box

### Changed

- Apply better workaround for Chroma startup wait
- Effects are now applied only when the game is in the foreground

## [1.0.1](https://github.com/poveden/EliteChroma/compare/v1.0.0...v1.0.1) — 2019-12-29

### Fixed

- Update `README.md` to point to the correct .NET Core runtime installer

### Changed

- Update solution to .NET Core 3.1

## [1.0.0](https://github.com/poveden/EliteChroma/releases/tag/v1.0.0) — 2019-09-28

Initial release.
