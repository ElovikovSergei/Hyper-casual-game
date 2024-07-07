using Core.Inventory;
using UnityEngine;

namespace Core.Controllers
{
    public sealed class ItemController : MonoBehaviour
    {
        [field: SerializeField] public ItemType ItemType;

        public void Setup(ItemType itemType)
        {
            ItemType = itemType;
        }
    }
}