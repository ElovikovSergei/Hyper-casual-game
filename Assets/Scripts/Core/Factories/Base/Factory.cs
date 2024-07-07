using UnityEngine.Pool;
using UnityEngine;

namespace Core.Factories
{
    public abstract class Factory<T, D> where T : MonoBehaviour
    {
        protected ObjectPool<T> pool;

        public Factory()
        {
            Initialize();
        }

        public abstract T GetInstance(D data);
        protected abstract void Initialize();
    }
}