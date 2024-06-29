using UnityEngine;

namespace Core
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : class
    {
        public static T Instance { get; protected set; }
    }
}