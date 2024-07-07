using Unity.AI.Navigation;
using UnityEngine;
using System;
using Tween;

namespace Core.Controllers
{
    public sealed class NavMeshController : MonoBehaviourSingleton<NavMeshController>
    {
        public bool IsNavMeshRecalculating { get; private set; }
        public bool IsNavMeshCalculated { get; private set; }

        public event Action OnNavMeshRecalculated;

        [SerializeField] private NavMeshSurface _surface;
        private TweenCase _calculatingTweenCase;

        public void CalculateSurface(Action callback = null)
        {
            if (IsNavMeshRecalculating)
                return;

            IsNavMeshRecalculating = true;

            _calculatingTweenCase = new NavMeshSurfaceTweenCase(_surface)
                .OnComplete(delegate
                {
                    IsNavMeshCalculated = true;
                    IsNavMeshRecalculating = false;

                    _calculatingTweenCase = null;

                    callback?.Invoke();
                    OnNavMeshRecalculated?.Invoke();
                })
                .StartTween();
        }

        private void Awake()
        {
            Instance = this;
            CalculateSurface();
        }
    }
}