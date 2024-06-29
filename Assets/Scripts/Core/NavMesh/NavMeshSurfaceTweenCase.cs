using Unity.AI.Navigation;
using UnityEngine;
using Tween;

namespace Core
{
    public sealed class NavMeshSurfaceTweenCase : TweenCase
    {
        private NavMeshSurface _navMeshSurface;
        private AsyncOperation _asyncOperation;

        public NavMeshSurfaceTweenCase(NavMeshSurface navMeshSurface)
        {
            _navMeshSurface = navMeshSurface;

            delay = 0f;
            duration = float.MaxValue;
            isUnscaled = true;

            _asyncOperation = navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
        }

        public override void DefaultComplete()
        {

        }

        public override void Invoke(float deltaTime)
        {
            if (!_asyncOperation.isDone)
                return;

            Complete();
        }

        public override bool Validate()
        {
            return true;
        }
    }
}