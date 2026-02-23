using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using HTDA.Framework.Events;

namespace HTDA.Framework.SceneFlow
{
    public sealed class SceneLoader : ISceneLoader
    {
        private readonly IEventBus _bus;

        public SceneLoader(IEventBus bus) => _bus = bus;

        public async Task LoadSceneAsync(
            string sceneName,
            LoadSceneMode mode = LoadSceneMode.Single,
            SceneFlowOptions options = default,
            SceneHooks hooks = null,
            Action<float> progress01 = null,
            CancellationToken ct = default)
        {
            options = options.Equals(default(SceneFlowOptions)) ? SceneFlowOptions.Default : options;
            hooks ??= new SceneHooks();

            bool isAdditive = mode == LoadSceneMode.Additive;
            _bus?.Publish(new SceneLoadStarted(sceneName, isAdditive));
            await SceneHooks.SafeInvoke(hooks.BeforeAsync);

            float elapsed = 0f;

            var op = SceneManager.LoadSceneAsync(sceneName, mode);
            if (op == null) return;

            op.allowSceneActivation = options.AllowSceneActivation;

            while (!op.isDone)
            {
                ct.ThrowIfCancellationRequested();

                // Unity progress: 0..0.9 while loading, then 0.9..1 when activating
                float p = op.progress;
                float normalized = options.AllowSceneActivation
                    ? Mathf.Clamp01(p)
                    : Mathf.Clamp01(p / 0.9f);

                // Enforce min loading time if set
                if (options.MinLoadingTime > 0f)
                {
                    elapsed += Time.unscaledDeltaTime;
                    float timeFactor = Mathf.Clamp01(elapsed / options.MinLoadingTime);
                    normalized = Mathf.Min(normalized, timeFactor);
                }

                progress01?.Invoke(normalized);
                _bus?.Publish(new SceneLoadProgress(sceneName, normalized));

                // If not allowing activation: when loaded (~0.9) we can run hooks then allow activation
                if (!options.AllowSceneActivation && op.progress >= 0.9f)
                {
                    await SceneHooks.SafeInvoke(hooks.AfterLoadAsync);
                    await SceneHooks.SafeInvoke(hooks.BeforeActivateAsync);

                    op.allowSceneActivation = true; // proceed activation
                    // AfterActivateAsync will run after op done below
                }

                await Task.Yield();
            }

            // At this point, scene is loaded & activated
            if (options.SetActiveOnLoad)
            {
                var scene = SceneManager.GetSceneByName(sceneName);
                if (scene.IsValid())
                    SceneManager.SetActiveScene(scene);
            }

            await SceneHooks.SafeInvoke(hooks.AfterActivateAsync);

            progress01?.Invoke(1f);
            _bus?.Publish(new SceneLoadProgress(sceneName, 1f));
            _bus?.Publish(new SceneLoaded(sceneName));
        }

        public async Task UnloadSceneAsync(
            string sceneName,
            SceneFlowOptions options = default,
            SceneHooks hooks = null,
            Action<float> progress01 = null,
            CancellationToken ct = default)
        {
            hooks ??= new SceneHooks();

            await SceneHooks.SafeInvoke(hooks.BeforeUnloadAsync);

            var op = SceneManager.UnloadSceneAsync(sceneName);
            if (op == null) return;

            while (!op.isDone)
            {
                ct.ThrowIfCancellationRequested();
                float p = Mathf.Clamp01(op.progress);
                progress01?.Invoke(p);
                await Task.Yield();
            }

            await SceneHooks.SafeInvoke(hooks.AfterUnloadAsync);

            progress01?.Invoke(1f);
            _bus?.Publish(new SceneUnloaded(sceneName));
        }
    }
}