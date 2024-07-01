using System;

namespace Core.Inventory
{
    [Serializable]
    public sealed class InventorySlotData
    {
        public ItemType ItemType;
        public int Amount;
    }
}