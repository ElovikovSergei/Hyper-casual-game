using UnityEngine;

namespace Tween
{
    [System.Serializable]
    public class CustomEasingFunction : Ease.IEasingFunction
    {
        [SerializeField] string name;
        public string Name => name;

        [SerializeField] AnimationCurve easingCurve;

        private float totalEasingTime;

        public void Initialize()
        {
            totalEasingTime = easingCurve.keys[easingCurve.keys.Length - 1].time;
        }

        public float Interpolate(float p)
        {
            return easingCurve.Evaluate(p * totalEasingTime);
        }
    }
}