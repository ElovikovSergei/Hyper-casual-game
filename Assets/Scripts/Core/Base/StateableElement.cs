using UnityEngine;

namespace Core
{
    public abstract class StateableElement : MonoBehaviour
    {
        public bool IsActive { get; private set; }

        public void SetActiveState(bool state)
        {
            IsActive = state;
        }
    }
}