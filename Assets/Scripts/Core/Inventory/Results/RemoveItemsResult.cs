namespace Core.Inventory
{
    public struct RemoveItemsResult
    {
        public readonly string InventoryOwnerId;
        public readonly int ItemsAmount;
        public readonly bool IsSuccess;

        public RemoveItemsResult(string inventoryOwnerId, int itemsAmount, bool isSuccess)
        {
            InventoryOwnerId = inventoryOwnerId;
            ItemsAmount = itemsAmount;
            IsSuccess = isSuccess;
        }
    }
}