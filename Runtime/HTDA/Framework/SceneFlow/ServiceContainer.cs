using System;
using System.Collections.Generic;

namespace HTDA.Framework.SceneFlow
{
    public sealed class ServiceContainer
    {
        private readonly Dictionary<Type, object> _map = new Dictionary<Type, object>(16);

        public void Register<T>(T instance) where T : class
        {
            if (instance == null) return;
            _map[typeof(T)] = instance;
        }

        public T Get<T>() where T : class
        {
            return _map.TryGetValue(typeof(T), out var obj) ? (T)obj : null;
        }

        public bool TryGet<T>(out T instance) where T : class
        {
            instance = Get<T>();
            return instance != null;
        }

        public void Clear() => _map.Clear();
    }
}