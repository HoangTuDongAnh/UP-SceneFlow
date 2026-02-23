using UnityEngine;

namespace HTDA.Framework.SceneFlow.Unity
{
    public sealed class DontDestroyRoot : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}