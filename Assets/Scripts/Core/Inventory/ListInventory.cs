using System.Collections.Generic;
using System.Linq;
using System;

namespace Core.Inventory
{
    public sealed class ListInventory : IListInventory
    {
        public string OwnerId => _data.OwnerId;

        public event Action<ItemType, int> OnItemAddedEvent;
        public event Action<ItemType, int> OnItemRemovedEvent;

        private readonly ListInventoryData _data;
        private readonly List<InventorySlot> _slots;

        public ListInventory(ListInventoryData data)
        {
            _slots = new List<InventorySlot>();
            _data = data;

            foreach (var slotData in _data.SlotsData)
            {
                _slots.Add(new InventorySlot(slotData));
            }
        }

        public RemoveItemsResult RemoveItems(ItemType itemType, int amount)
        {
            if (!HasItems(itemType, amount))
                return new RemoveItemsResult(OwnerId, amount, false);

            var slot = _slots.First(slot => slot.ItemType == itemType);

            slot.Amount -= amount;

            if (slot.Amount == 0)
            {
                slot.ItemType = ItemType.None;
                _slots.Remove(slot);
            }

            return new RemoveItemsResult(OwnerId, amount, true);
        }

        public AddItemsResult AddItems(ItemType itemType, int amount)
        {
            var slot = _slots.FirstOrDefault(slot => slot.ItemType == itemType);
            var itemsSlotCapacity = 2; // TO DO: [test] replace this
            var addedItemsAmount = 0;
            
            if (slot == null)
            {
                slot = new InventorySlot(new InventorySlotData
                {
                    ItemType = itemType
                });
                _slots.Add(slot);
            }

            var newValue = slot.Amount + amount;

            if (newValue > itemsSlotCapacity)
            {
                addedItemsAmount += (itemsSlotCapacity - slot.Amount);
                slot.Amount = itemsSlotCapacity;
            }
            else
            {
                addedItemsAmount = amount;
                slot.Amount = newValue;
            }

            return new AddItemsResult(OwnerId, amount, addedItemsAmount);
        }

        public bool HasItems(ItemType itemType, int amount)
        {
            return GetItemsAmount(itemType) >= amount;
        }

        public int GetItemsAmount(ItemType itemType)
        {
            var amount = 0;
            var slots = _data.SlotsData;

            foreach (var slot in slots)
            {
                if (slot.ItemType != itemType)
                    continue;

                amount += slot.Amount;
            }

            return amount;
        }

        public IInventorySlot[] GetSlots()
        {
            var slots = new IInventorySlot[_slots.Count];

            for (int i = 0; i < slots.Length; ++i)
            {
                slots[i] = _slots[i];
            }

            return slots;
        }
    }
}