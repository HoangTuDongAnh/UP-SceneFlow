namespace HTDA.Framework.SceneFlow
{
    public readonly struct SceneFlowOptions
    {
        public readonly bool SetActiveOnLoad;
        public readonly bool AllowSceneActivation;
        public readonly float MinLoadingTime;

        public SceneFlowOptions(
            bool setActiveOnLoad = true,
            bool allowSceneActivation = true,
            float minLoadingTime = 0f)
        {
            SetActiveOnLoad = setActiveOnLoad;
            AllowSceneActivation = allowSceneActivation;
            MinLoadingTime = minLoadingTime < 0 ? 0 : minLoadingTime;
        }

        public static SceneFlowOptions Default => new SceneFlowOptions();
    }
}