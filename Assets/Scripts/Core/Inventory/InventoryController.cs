using UnityEngine;

namespace Core.Inventory
{
    public sealed class InventoryController : MonoBehaviour
    {
        public IInventory Inventory { get; private set; }

        public void Setup(IInventory inventory)
        {
            if (Inventory != null)
                return;

            Inventory = inventory;
        }
    }
}