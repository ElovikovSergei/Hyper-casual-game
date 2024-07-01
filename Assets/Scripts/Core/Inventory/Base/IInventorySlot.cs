using System;

namespace Core.Inventory
{
    public interface IInventorySlot
    {
        public ItemType ItemType { get; }
        public bool IsEmpty { get; }
        public int Amount { get; }

        public event Action<ItemType> OnItemTypeChangedEvent;
        public event Action<int> OnItemAmountChangedEvent;
    }
}