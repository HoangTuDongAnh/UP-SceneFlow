using UnityEngine;
using HTDA.Framework.Events;
using HTDA.Framework.SceneFlow.Unity;
using HTDA.Framework.Core.Primitives;

namespace HTDA.Framework.SceneFlow
{
    /// <summary>
    /// Entry point for scene flow. Place this in your Boot scene.
    /// Creates a ServiceRegistry and registers core services:
    /// - IEventBus
    /// - ISceneLoader
    /// </summary>
    public sealed class GameBootstrap : MonoBehaviour
    {
        [Header("Optional: create a persistent root")]
        [SerializeField] private bool createPersistentRoot = true;

        [Header("Optional: auto load first scene")]
        [SerializeField] private bool autoLoadFirstScene = false;
        [SerializeField] private string firstSceneName = "Loading";

        public static ServiceRegistry Services { get; private set; }

        private void Awake()
        {
            if (Services != null) return;

            if (createPersistentRoot)
            {
                var root = new GameObject("[UP] PersistentRoot");
                root.AddComponent<DontDestroyRoot>();
            }

            Services = new ServiceRegistry();

            var bus = new EventBus();
            Services.Register<IEventBus>(bus);

            var sceneLoader = new SceneLoader(bus);
            Services.Register<ISceneLoader>(sceneLoader);

            if (autoLoadFirstScene && !string.IsNullOrEmpty(firstSceneName))
            {
                _ = sceneLoader.LoadSceneAsync(firstSceneName);
            }
        }
    }
}