namespace HTDA.Framework.SceneFlow
{
    public readonly struct SceneLoadStarted
    {
        public readonly string SceneName;
        public readonly bool IsAdditive;
        public SceneLoadStarted(string sceneName, bool isAdditive)
        {
            SceneName = sceneName;
            IsAdditive = isAdditive;
        }
    }

    public readonly struct SceneLoadProgress
    {
        public readonly string SceneName;
        public readonly float Progress01;
        public SceneLoadProgress(string sceneName, float progress01)
        {
            SceneName = sceneName;
            Progress01 = progress01;
        }
    }

    public readonly struct SceneLoaded
    {
        public readonly string SceneName;
        public SceneLoaded(string sceneName) => SceneName = sceneName;
    }

    public readonly struct SceneUnloaded
    {
        public readonly string SceneName;
        public SceneUnloaded(string sceneName) => SceneName = sceneName;
    }
}