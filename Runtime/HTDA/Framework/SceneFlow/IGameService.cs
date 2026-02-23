using HTDA.Framework.Core.Primitives;

namespace HTDA.Framework.SceneFlow
{
    public interface IGameService
    {
        void Initialize(ServiceRegistry services);
        void Shutdown();
    }
}