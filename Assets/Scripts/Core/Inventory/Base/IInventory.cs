using System;

namespace Core.Inventory
{
    public interface IInventory
    {
        public string OwnerId { get; }

        public event Action<ItemType, int> OnItemAddedEvent;
        public event Action<ItemType, int> OnItemRemovedEvent;

        public RemoveItemsResult RemoveItems(ItemType itemType, int amount);
        public AddItemsResult AddItems(ItemType itemType, int amount);
        public bool HasItems(ItemType itemType, int amount);
        public int GetItemsAmount(ItemType itemType);
    }
}