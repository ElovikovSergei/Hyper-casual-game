using UnityEngine;

namespace Core.Entities
{
    public abstract class PersonController : MonoBehaviour, IMovable, IRotatable
    {
        public void Move(Vector3 direction)
        {

        }

        public void Rotate(Vector3 direction)
        {

        }
    }
}