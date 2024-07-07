using Core.Inventory;
using UnityEngine;

namespace Core.Controllers
{
    public sealed class ItemController : MonoBehaviour
    {
        [field: SerializeField] public ItemType ItemType { get; private set; }

        public void Setup(ItemType itemType)
        {
            ItemType = itemType;
        }
    }
}