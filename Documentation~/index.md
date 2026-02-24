# UP-SceneFlow – Documentation

## 1. Bootstrap

`GameBootstrap` tạo:
- `ServiceRegistry` (từ UP-Core)
- `IEventBus`
- `ISceneLoader`

Khuyến nghị:
- Đặt GameBootstrap trong scene Boot (scene entry).
- Tạo PersistentRoot (DontDestroyOnLoad) cho các system sống xuyên scene.

## 2. SceneLoader

### LoadSceneAsync
- Tham số:
    - sceneName
    - mode (Single/Additive)
    - options (SetActiveOnLoad, AllowSceneActivation, MinLoadingTime)
    - hooks (Before/After/Activate/Unload)
    - progress callback (0..1)
    - CancellationToken

### Progress normalization
Unity async progress thường 0..0.9 trước khi activation.
SceneFlow chuẩn hoá về 0..1 để UI dễ dùng.

### MinLoadingTime
Dùng để demo/prod tránh loading nháy:
- ép UI loading hiển thị tối thiểu X giây.

## 3. Events (UP-Events)

SceneFlow publish:
- SceneLoadStarted
- SceneLoadProgress
- SceneLoaded
- SceneUnloaded

UI loading chỉ cần subscribe `SceneLoadProgress` để update progress bar.

## 4. Sample notes

- Loading scene chỉ nên chứa UI (không camera/audio listener).
- Gameplay scene có camera + AudioListener duy nhất.
- Nếu project bật New Input System, sample không nên dùng `UnityEngine.Input`.
