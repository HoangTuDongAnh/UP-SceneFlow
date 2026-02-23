using UnityEngine;
using UnityEngine.SceneManagement;

namespace HTDA.Framework.SceneFlow.Samples
{
    public sealed class DemoGameplayUI : MonoBehaviour
    {
        public string bootScene = "Boot";

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(bootScene);
            }
        }
    }
}