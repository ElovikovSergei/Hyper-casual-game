using UnityEngine.UI;
using UnityEngine;

namespace Tween
{
    [RequireComponent(typeof(Graphic))]
    public sealed class DoColorTween : DoTween<Graphic, Color>
    {
        protected override Color TargetValue
        {
            get => TargetComponent.color;
            set => TargetComponent.color = value;
        }

        protected override void StartLoop(float delay)
        {
            TargetValue = startValue;
            tweenCase = TargetComponent.DOColor(endValue, duration);

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
