using UnityEngine;
using HTDA.Framework.Events;
using HTDA.Framework.SceneFlow.Unity;

namespace HTDA.Framework.SceneFlow
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [Header("Optional: create a persistent root")]
        [SerializeField] private bool createPersistentRoot = true;

        [Header("Optional: auto load first scene")]
        [SerializeField] private bool autoLoadFirstScene = false;
        [SerializeField] private string firstSceneName = "Loading";

        public static ServiceContainer Services { get; private set; }

        private void Awake()
        {
            if (Services != null) return;

            if (createPersistentRoot)
            {
                var root = new GameObject("[UP] PersistentRoot");
                root.AddComponent<DontDestroyRoot>();
            }

            Services = new ServiceContainer();

            // Init core services
            var bus = new EventBus();
            Services.Register<IEventBus>(bus);

            var sceneLoader = new SceneLoader(bus);
            Services.Register<ISceneLoader>(sceneLoader);

            // TODO: if you want, init Settings/Core services here too (your framework)
            // Example: Services.Register<MySettingsService>(...);

            if (autoLoadFirstScene && !string.IsNullOrEmpty(firstSceneName))
            {
                // Fire and forget is okay for bootstrap demo; production: store task or use async runner
                _ = sceneLoader.LoadSceneAsync(firstSceneName);
            }
        }

        private void OnDestroy()
        {
            // Usually bootstrap persists; but if destroyed, clear
            // Services?.Clear();
            // Services = null;
        }
    }
}