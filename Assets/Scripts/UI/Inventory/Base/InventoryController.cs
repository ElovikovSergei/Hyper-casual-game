using Core.Inventory;
using UnityEngine;

namespace UI.Inventory
{
    public abstract class InventoryController<T> : MonoBehaviour where T : IInventory
    {
        [SerializeField] private InventorySlotController[] _slotsControllers;

        public abstract void Setup(T inventory);

        protected InventorySlotController GetSlotController(int index)
        {
            return _slotsControllers[index];
        }
    }
}