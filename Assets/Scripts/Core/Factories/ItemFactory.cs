using Core.Controllers;
using Core.Inventory;

namespace Core.Factories
{
    public sealed class ItemFactory : Factory<ItemController, ItemType>
    {
        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public override ItemController GetInstance(ItemType itemType)
        {
            throw new System.Exception();
        }
    }
}