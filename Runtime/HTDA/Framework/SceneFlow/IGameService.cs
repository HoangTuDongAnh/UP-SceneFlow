namespace HTDA.Framework.SceneFlow
{
    public interface IGameService
    {
        void Initialize(ServiceContainer services);
        void Shutdown();
    }
}