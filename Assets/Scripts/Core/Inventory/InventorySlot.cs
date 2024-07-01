using System;

namespace Core.Inventory
{
    public sealed class InventorySlot : IInventorySlot
    {
        public ItemType ItemType
        {
            get => _data.ItemType;
            set
            {
                if (_data.ItemType == value)
                    return;

                _data.ItemType = value;
                OnItemTypeChangedEvent?.Invoke(value);
            }
        }
        public int Amount
        {
            get => _data.Amount;
            set
            {
                if (_data.Amount == value)
                    return;

                _data.Amount = value;
            }
        }
        public bool IsEmpty => ItemType == default && Amount == 0;

        public event Action<ItemType> OnItemTypeChangedEvent;
        public event Action<int> OnItemAmountChangedEvent;

        private readonly InventorySlotData _data;

        public InventorySlot(InventorySlotData data)
        {
            _data = data;
        }
    }
}