using Core.Inventory;

namespace UI.Inventory
{
    public class ListInventoryController : InventoryController<IListInventory>
    {
        public override void Setup(IListInventory inventory)
        {
            var slots = inventory.GetSlots();

            for (int i = 0; i < slots.Length; ++i)
            {
                GetSlotController(i).Setup(slots[i]);
            }
        }
    }
}