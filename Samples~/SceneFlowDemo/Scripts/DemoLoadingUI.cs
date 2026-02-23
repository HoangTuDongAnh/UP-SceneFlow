using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HTDA.Framework.Events;
using HTDA.Framework.SceneFlow;

namespace HTDA.Framework.SceneFlow.Samples
{
    public sealed class DemoLoadingUI : MonoBehaviour
    {
        public Slider progressSlider;
        public TMP_Text progressText;

        private System.IDisposable _subscription;

        private void Start()
        {
            var bus = GameBootstrap.Services.Get<IEventBus>();
            if (bus == null) return;

            _subscription = bus.Subscribe<SceneLoadProgress>(OnProgress);
        }

        private void OnProgress(SceneLoadProgress e)
        {
            if (progressSlider != null)
                progressSlider.value = e.Progress01;

            if (progressText != null)
                progressText.text = $"{(e.Progress01 * 100f):0}%";
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}