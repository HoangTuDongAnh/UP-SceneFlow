using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using HTDA.Framework.SceneFlow;

namespace HTDA.Framework.SceneFlow.Samples
{
    public sealed class DemoFlowController : MonoBehaviour
    {
        public string loadingScene = "Loading";
        public string gameplayScene = "Gameplay";

        [Range(0.5f, 10f)]
        public float minLoadingTime = 3f;

        [Range(0f, 5f)]
        public float holdBeforeActivateSeconds = 1.5f;

        private ISceneLoader _loader;

        private async void Start()
        {
            _loader = GameBootstrap.Services.Get<ISceneLoader>();

            await _loader.LoadSceneAsync(loadingScene, LoadSceneMode.Additive);

            var hooks = new SceneHooks
            {
                // Called when load reached ~0.9 and activation is still blocked
                BeforeActivateAsync = async () =>
                {
                    if (holdBeforeActivateSeconds > 0f)
                        await Task.Delay((int)(holdBeforeActivateSeconds * 1000f));
                }
            };

            await _loader.LoadSceneAsync(
                gameplayScene,
                LoadSceneMode.Additive,
                new SceneFlowOptions(
                    setActiveOnLoad: true,
                    allowSceneActivation: false, // block at 0.9
                    minLoadingTime: minLoadingTime
                ),
                hooks: hooks
            );

            await _loader.UnloadSceneAsync(loadingScene);
        }
    }
}