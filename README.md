# TemplateProjectUnity

A Unity project template for building games on top of reusable in-house modules.

## Features

- Game state machine (`GameStateMachine`, `IGameState`, `BaseGameState`) wired through VContainer.
- Scene scopes for a loading scene and a main scene (`LoadingSceneScope`, `MainSceneScope`).
- Placeholder UI views: loading, gameplay, win, lose and three popups.
- Local user data and CSV level data loading (`UserLocalData`, `LevelCsv`).
- Foundation and Features modules pulled in as git submodules (`tvan.uni.foundation`, `tvan.uni.features`).

## Tech stack

- Unity 6000.1.3f1, C#
- VContainer for dependency injection
- UniTask, MessagePipe, Addressables, DOTween
- NuGetForUnity (R3, Sylvan.Data.Csv and others under `Assets/Packages`)

## Getting started

```bash
git clone --recurse-submodules -b develop git@github.com:tvanle/TemplateProjectUnity.git
# or, in an existing clone:
git submodule update --init --recursive
```

Open the folder with Unity Hub using editor version 6000.1.3f1, then open the loading scene and press Play.
