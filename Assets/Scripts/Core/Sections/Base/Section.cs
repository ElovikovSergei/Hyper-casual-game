using UnityEngine;

namespace Core.Sections
{
    public abstract class Section : StateableElement
    {
        protected virtual void OnTriggerEnter(Collider other) { }
        protected virtual void OnTriggerExit(Collider other) { }
    }
}