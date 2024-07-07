using UnityEngine;

namespace Core
{
    public static class CoreExtensions
    {
        /// <summary>
        /// Reset transforms local position, rotation and scale
        /// </summary>
        public static Transform ResetLocal(this Transform transform)
        {
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            return transform;
        }
    }
}