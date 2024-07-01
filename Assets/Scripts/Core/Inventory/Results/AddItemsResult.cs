namespace Core.Inventory
{
    public struct AddItemsResult
    {
        public int ItemsNotAddedAmount => ItemsAmount - AddedItemsAmount;

        public readonly string InventoryOwnerId;
        public readonly int AddedItemsAmount;
        public readonly int ItemsAmount;

        public AddItemsResult(string inventoryOwnerId, int itemsAmount, int addedItemsAmount)
        {
            InventoryOwnerId = inventoryOwnerId;
            AddedItemsAmount = addedItemsAmount;
            ItemsAmount = itemsAmount;
        }
    }
}