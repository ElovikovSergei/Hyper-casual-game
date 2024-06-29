using UnityEngine;

namespace Tween
{
    [CreateAssetMenu(fileName = "Easing Settings", menuName = "Settings/Tween Easing Settings")]
    public sealed class EasingSettings : ScriptableObject
    {
        [SerializeField] CustomEasingFunction[] customEasingFunctions;
        public CustomEasingFunction[] CustomEasingFunctions => customEasingFunctions;
    }
}