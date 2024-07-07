using UnityEngine.AI;
using Core.Inventory;
using UnityEngine;

namespace Core.Entities
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class PersonController : MonoBehaviour, IMovable
    {
        protected NavMeshAgent Agent => _agent;

        [SerializeField] private InventoryController _inventory;
        [SerializeField] private NavMeshAgent _agent;

        public virtual void Move(Vector3 direction) { }
    }
}