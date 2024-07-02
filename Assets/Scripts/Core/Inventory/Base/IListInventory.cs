namespace Core.Inventory
{
    public interface IListInventory : IInventory
    {
        public IInventorySlot[] GetSlots();
    }
}