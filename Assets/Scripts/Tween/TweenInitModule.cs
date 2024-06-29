using UnityEngine;

namespace Tween
{
    public sealed class TweenInitModule : MonoBehaviour
    {
        [SerializeField] private EasingSettings _easingSettings;

        [Space]
        [SerializeField] private int _tweensUpdateCount = 300;
        [SerializeField] private int _tweensFixedUpdateCount = 30;
        [SerializeField] private int _tweensLateUpdateCount = 0;

        [Space]
        [SerializeField] private bool _enableSystemLogs;

        private void Awake()
        {
            var tween = gameObject.AddComponent<TweenController>();

            tween.Init(_tweensUpdateCount, _tweensFixedUpdateCount, _tweensLateUpdateCount, _enableSystemLogs);
            Ease.Initialize(_easingSettings);
        }
    }
}