# UP-SceneFlow (HTDA.Framework.SceneFlow)

Chuẩn hoá flow: bootstrap → load scene → loading screen → gameplay → unload.

## Features

- Bootstrap: `GameBootstrap` tạo service registry và init service cơ bản
- Scene API: `ISceneLoader` / `SceneLoader`
- Loading pipeline:
    - progress 0..1 (normalized)
    - hooks trước/sau load
    - `MinLoadingTime` để tránh loading quá nhanh
- Flow events qua UP-Events:
    - `SceneLoadStarted`
    - `SceneLoadProgress`
    - `SceneLoaded`
    - `SceneUnloaded`
- Sample: SceneFlowDemo (Boot → Loading → Gameplay)

## Quick start

```csharp
var loader = GameBootstrap.Services.Get<ISceneLoader>();
await loader.LoadSceneAsync("Gameplay");
```

## Notes

- Với Additive loading, hãy đảm bảo chỉ có **1 AudioListener** hoạt động tại một thời điểm.
- Sample nên dùng UI Button thay vì `UnityEngine.Input` để tương thích cả Legacy + New Input System.
