using Core.Controllers;
using Core.Inventory;

namespace Core.Factories
{
    public sealed class ItemFactory : Factory<ItemController, ItemType>
    {
        public override ItemController GetInstance(ItemType itemType)
        {
            throw new System.Exception();
        }
        
        protected override void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}