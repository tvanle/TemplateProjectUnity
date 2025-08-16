# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Unity 6000.0.38f1 project template for hypercasual game development with dependency injection and state machine patterns.

## Key Technologies

- **Unity Version**: 6000.0.38f1
- **Primary DI Framework**: Zenject (Extenject) and VContainer
- **Async Operations**: UniTask
- **Tweening**: DOTween Pro
- **UI Management**: Custom UniUI framework
- **State Management**: Custom state machine pattern
- **Addressables**: Unity Addressables 2.4.6
- **Messaging**: MessagePipe

## Build and Development Commands

### Unity Editor Operations
- Open project in Unity Hub with Unity 6000.0.38f1
- Play Mode: Use Unity Editor play button or Ctrl+P
- Build: File → Build Settings → Build (or Ctrl+Shift+B)

### Common Development Tasks
```bash
# Open Unity project (Windows)
"C:\Program Files\Unity\Hub\Editor\6000.0.38f1\Editor\Unity.exe" -projectPath "C:\Users\PC\Documents\Free\TemplateProjectUnity"

# Git operations
git status
git add .
git commit -m "commit message"
git push origin develop
```

## Project Architecture

### Assembly Structure
- **GamePlay.Script**: Main gameplay code assembly
  - References: UniTask, DOTween, Zenject, UniCore modules
  - Contains game state machine, scene installers, UI views
  
### Core Frameworks
- **UniCore**: Foundation utilities (AssetLibrary, Extensions, ObjectPool, SignalBus, StateMachine)
- **UniUI**: UI management framework for screens and popups
- **UniData**: Data management layer

### Scene Architecture
1. **LoadingScene** (0.LoadingScene.unity): Initial loading and initialization
2. **MainScene** (1.MainScene.unity): Main gameplay scene

### Dependency Injection Pattern
- Scene-specific installers in `Assets/Scripts/Scenes/*/`
- Project context configured via `Resources/ProjectContext.prefab`
- Game state machine installer at `Assets/Scripts/StateMachine/GameStateMachineInstaller.cs`

### UI System
- Base classes: `HyperCasualUIBaseScreen`, `HyperCasualUIBasePopup`
- Screen views in `Assets/Scripts/UI/Loading/`
- Root UI prefab: `Assets/_Prefabs/Base/HyperCasualRootUI.prefab`

## Key Directories

- `Assets/Scripts/` - Main game scripts
  - `Data/` - Data models and controllers
  - `Scenes/` - Scene-specific code and installers
  - `StateMachine/` - Game state management
  - `UI/` - UI view controllers
- `Assets/Resources/` - Runtime-loaded assets
  - `CsvData/` - CSV data files (e.g., Level.csv)
- `Assets/_Prefabs/` - Prefab templates
- `Assets/Packages/Tvan/` - Custom framework packages
- `Assets/AddressableAssetsData/` - Addressables configuration

## Package Dependencies

### Third-party Packages (via OpenUPM)
- MessagePipe 1.8.1 - Pub/sub messaging
- UniTask - Async/await for Unity
- Zenject/Extenject - Dependency injection
- VContainer 1.16.8 - Alternative DI container

### Unity Packages
- Addressables 2.4.6 - Asset management
- 2D Feature set - Complete 2D tooling
- Timeline 1.8.7 - Cutscene tools
- TextMeshPro - Advanced text rendering
- Navigation AI 2.0.7 - Pathfinding

## CSV Data Management
- CSV files located in `Assets/Resources/CsvData/`
- Parsed using Sylvan.Data.Csv library
- Example: `LevelCsv.cs` for level data parsing

## Asset Loading Pattern
- Use `LoadImageHelper` for loading and caching images from URLs
- Sprites cached in memory to avoid repeated downloads
- Located at `Assets/Scripts/Scenes/MainScene/Utils/LoadImageHelper.cs`

## Testing Approach
Check for test assemblies and runners in the project. Use Unity Test Framework for unit and integration tests when available.

## Important Notes
- Main branch is `develop` (not `main` or `master`)
- Project uses both Zenject and VContainer - check scene context for which is active
- DOTween Pro is included - use for all animation needs
- R3 (Reactive Extensions) available for reactive programming patterns