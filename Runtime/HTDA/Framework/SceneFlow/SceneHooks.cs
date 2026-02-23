using System;
using System.Threading.Tasks;

namespace HTDA.Framework.SceneFlow
{
    public sealed class SceneHooks
    {
        /// <summary>Before starting load/unload (e.g., fade out)</summary>
        public Func<Task> BeforeAsync;

        /// <summary>After scene loaded (before activation if AllowSceneActivation=false)</summary>
        public Func<Task> AfterLoadAsync;

        /// <summary>Right before scene activation (only meaningful when AllowSceneActivation=false)</summary>
        public Func<Task> BeforeActivateAsync;

        /// <summary>After activation + optionally SetActiveScene</summary>
        public Func<Task> AfterActivateAsync;

        /// <summary>Before unload starts</summary>
        public Func<Task> BeforeUnloadAsync;

        /// <summary>After unload done</summary>
        public Func<Task> AfterUnloadAsync;

        internal static Task SafeInvoke(Func<Task> fn)
            => fn != null ? fn() : Task.CompletedTask;
    }
}