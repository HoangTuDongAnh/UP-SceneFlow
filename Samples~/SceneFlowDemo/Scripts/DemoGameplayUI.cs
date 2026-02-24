using UnityEngine;
using UnityEngine.SceneManagement;

namespace HTDA.Framework.SceneFlow.Samples
{
    /// <summary>
    /// Hook Restart() to a UI Button OnClick to avoid dependency on Legacy Input.
    /// Compatible with both Legacy Input and New Input System projects.
    /// </summary>
    public sealed class DemoGameplayUI : MonoBehaviour
    {
        [SerializeField] private string bootScene = "Boot";

        public void Restart()
        {
            SceneManager.LoadScene(bootScene, LoadSceneMode.Single);
        }
    }
}