using Core.Inventory;

namespace UI.Inventory
{
    public sealed class GridInventoryController : InventoryController<IGridInventory>
    {
        public override void Setup(IGridInventory inventory)
        {
            var slots = inventory.GetSlots();

            for (int x = 0; x < inventory.Size.x; ++x)
            {
                for (int y = 0; y < inventory.Size.y; ++y)
                {
                    var index = x * inventory.Size.y + y;
                    var slotController = GetSlotController(index);

                    slotController.Setup(slots[x, y]);
                }
            }
        }
    }
}