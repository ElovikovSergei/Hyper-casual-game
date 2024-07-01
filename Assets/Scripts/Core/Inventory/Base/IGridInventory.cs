using System;
using UnityEngine;

namespace Core.Inventory
{
    public interface IGridInventory : IInventory
    {
        public Vector2Int Size { get; }

        public event Action<Vector2Int> OnSizeChangedEvent;

        public RemoveItemsResult RemoveItems(Vector2Int slotCoords, ItemType itemType, int amount);
        public AddItemsResult AddItems(Vector2Int slotCoords, ItemType itemType, int amount);
        public IInventorySlot[,] GetSlots();
    }
}