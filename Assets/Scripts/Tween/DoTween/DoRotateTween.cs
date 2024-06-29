using UnityEngine;

namespace Tween
{
    [RequireComponent(typeof(Transform))]
    public sealed class DoRotateTween : DoTween<Transform, Vector3>
    {
        protected override Vector3 TargetValue
        {
            get => TargetComponent.eulerAngles;
            set => TargetComponent.eulerAngles = value;
        }

        protected override void StartLoop(float delay)
        {
            TargetValue = startValue;
            tweenCase = TargetComponent.DORotate(endValue, duration);

            base.StartLoop(delay);
        }

        protected override void IncrementLoopChangeValues()
        {
            var difference = endValue - startValue;

            startValue = endValue;
            endValue += difference;
        }
    }
}
