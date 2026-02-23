using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace HTDA.Framework.SceneFlow
{
    public interface ISceneLoader
    {
        Task LoadSceneAsync(
            string sceneName,
            LoadSceneMode mode = LoadSceneMode.Single,
            SceneFlowOptions options = default,
            SceneHooks hooks = null,
            Action<float> progress01 = null,
            CancellationToken ct = default);

        Task UnloadSceneAsync(
            string sceneName,
            SceneFlowOptions options = default,
            SceneHooks hooks = null,
            Action<float> progress01 = null,
            CancellationToken ct = default);
    }
}