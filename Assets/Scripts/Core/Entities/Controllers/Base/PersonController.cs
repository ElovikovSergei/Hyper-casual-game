using UnityEngine.AI;
using UnityEngine;

namespace Core.Entities
{
    public abstract class PersonController : MonoBehaviour, IMovable
    {
        protected NavMeshAgent Agent => _agent;

        [SerializeField] private NavMeshAgent _agent;

        public virtual void Move(Vector3 direction) { }
    }
}