using System.Collections.Generic;
using UnityEngine;
using System;

namespace Core.Inventory
{
    public sealed class GridInventory : IGridInventory
    {
        public Vector2Int Size
        {
            get => _data.Size;
            set
            {
                if (_data.Size == value)
                    return;

                _data.Size = value;
                OnSizeChangedEvent?.Invoke(value);
            }
        }
        public string OwnerId => _data.OwnerId;

        public event Action<Vector2Int> OnSizeChangedEvent;
        public event Action<ItemType, int> OnItemAddedEvent;
        public event Action<ItemType, int> OnItemRemovedEvent;

        private readonly GridInventoryData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slots;

        public GridInventory(GridInventoryData data)
        {
            _slots = new Dictionary<Vector2Int, InventorySlot>();
            _data = data;

            for (int x = 0; x < Size.x; ++x)
            {
                for (int y = 0; y < Size.y; ++y)
                {
                    var key = new Vector2Int(x, y);
                    var index = x * Size.y + y;
                    var slot = new InventorySlot(_data.SlotsData[index]);

                    _slots[key] = slot;
                }
            }
        }

        public RemoveItemsResult RemoveItems(Vector2Int slotCoords, ItemType itemType, int amount = 1)
        {
            var slot = _slots[slotCoords];

            if (slot.IsEmpty || slot.ItemType != itemType || slot.Amount < amount)
                return new RemoveItemsResult(OwnerId, amount, false);

            slot.Amount -= amount;

            if (slot.Amount == 0)
                slot.ItemType = ItemType.None;

            return new RemoveItemsResult(OwnerId, amount, true);
        }

        public AddItemsResult AddItems(Vector2Int slotCoords, ItemType itemType, int amount = 1)
        {
            var addedItemsAmount = 0;
            var slot = _slots[slotCoords];
            var newAmount = slot.Amount + amount;

            if (slot.IsEmpty)
                slot.ItemType = itemType;

            var itemsSlotCapacity = 2; // TO DO: [test] replace this

            if (newAmount > itemsSlotCapacity)
            {
                var remainingItems = newAmount - itemsSlotCapacity;
                var itemsToAddAmount = itemsSlotCapacity - slot.Amount;

                addedItemsAmount += itemsToAddAmount;
                slot.Amount = itemsSlotCapacity;

                addedItemsAmount += AddItems(itemType, remainingItems).AddedItemsAmount;
            }
            else
            {
                addedItemsAmount = amount;
                slot.Amount = amount;
            }

            return new AddItemsResult(OwnerId, amount, addedItemsAmount);
        }

        public RemoveItemsResult RemoveItems(ItemType itemType, int amount = 1)
        {
            if (!HasItems(itemType, amount))
                return new RemoveItemsResult(OwnerId, amount, false);

            var remainingItemsAmount = amount;

            for (int x = 0; x < Size.x; ++x)
            {
                for (int y = 0; y < Size.y; ++y)
                {
                    if (remainingItemsAmount <= 0)
                        break;

                    var coords = new Vector2Int(x, y);
                    var slot = _slots[coords];

                    if (slot.ItemType != itemType)
                        continue;

                    if (remainingItemsAmount > slot.Amount)
                    {
                        remainingItemsAmount -= slot.Amount;
                        RemoveItems(coords, itemType, slot.Amount);
                    }
                    else
                    {
                        RemoveItems(coords, itemType, remainingItemsAmount);
                    }
                }
            }

            return new RemoveItemsResult(OwnerId, amount, true);
        }

        public AddItemsResult AddItems(ItemType itemType, int amount = 1)
        {
            var remainingItemsAmount = amount;
            var itemsAddedToExistingSlots = AddItemsToExistingSlots(itemType, remainingItemsAmount, out remainingItemsAmount);

            if (remainingItemsAmount <= 0)
                return new AddItemsResult(OwnerId, amount, itemsAddedToExistingSlots);

            var itemsAddedToEmptySlots = AddItemsToEmptySlots(itemType, remainingItemsAmount, out remainingItemsAmount);
            var totalAddedItemsAmount = itemsAddedToExistingSlots + itemsAddedToEmptySlots;

            return new AddItemsResult(OwnerId, amount, totalAddedItemsAmount);


            int AddItemsToExistingSlots(ItemType type, int amount, out int remainingItemsAmount)
            {
                var addedItemsAmount = 0;
                remainingItemsAmount = amount;

                for (int x = 0; x < Size.x; ++x)
                {
                    for (int y = 0; y < Size.y; ++y)
                    {
                        if (remainingItemsAmount <= 0)
                            break;

                        var slot = _slots[new Vector2Int(x, y)];

                        if (slot.IsEmpty || slot.ItemType != type)
                            continue;

                        var itemsSlotCapacity = 2; // TO DO: [test] replace this

                        if (slot.Amount >= itemsSlotCapacity)
                            continue;

                        var newValue = slot.Amount + remainingItemsAmount;

                        if (newValue > itemsSlotCapacity)
                        {
                            remainingItemsAmount = newValue - itemsSlotCapacity;
                            addedItemsAmount += (itemsSlotCapacity - slot.Amount);
                            slot.Amount = itemsSlotCapacity;
                        }
                        else
                        {
                            addedItemsAmount += remainingItemsAmount;
                            slot.Amount = newValue;
                            remainingItemsAmount = 0;
                        }
                    }
                }

                return addedItemsAmount;
            }
            int AddItemsToEmptySlots(ItemType type, int amount, out int remainingItemsAmount)
            {
                var addedItemsAmount = 0;
                remainingItemsAmount = amount;

                for (int x = 0; x < Size.x; ++x)
                {
                    for (int y = 0; y < Size.y; ++y)
                    {
                        if (remainingItemsAmount <= 0)
                            break;

                        var slot = _slots[new Vector2Int(x, y)];

                        if (slot.IsEmpty)
                            continue;

                        slot.ItemType = type;

                        var itemsSlotCapacity = 2; // TO DO: [test] replace this
                        var newValue = remainingItemsAmount;

                        if (newValue > itemsSlotCapacity)
                        {
                            remainingItemsAmount = newValue - itemsSlotCapacity;
                            addedItemsAmount += itemsSlotCapacity;
                            slot.Amount = itemsSlotCapacity;
                        }
                        else
                        {
                            addedItemsAmount += remainingItemsAmount;
                            slot.Amount = newValue;
                            remainingItemsAmount = 0;
                        }
                    }
                }

                return addedItemsAmount;
            }
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

        public IInventorySlot[,] GetSlots()
        {
            var slots = new IInventorySlot[Size.x, Size.y];

            for (int x = 0; x < Size.x; ++x)
            {
                for (int y = 0; y < Size.y; ++y)
                {
                    slots[x, y] = _slots[new Vector2Int(x, y)];
                }
            }

            return slots;
        }
    }
}