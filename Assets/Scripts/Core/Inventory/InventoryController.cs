using System.Collections.Generic;
using Core.Controllers;
using System.Linq;
using UnityEngine;
using Extensions;
using Tween;

namespace Core.Inventory
{
    public sealed class InventoryController : MonoBehaviour
    {
        [field: SerializeField] public Transform ItemsInstanceParent { get; private set; }
        public IInventory Inventory { get; private set; }

#if UNITY_EDITOR
        [ReadOnly]
#endif
        [SerializeField] private List<ItemController> _itemControllers;

        public void Setup(IInventory inventory)
        {
            if (Inventory != null)
                return;

            Inventory = inventory;

            Inventory.OnItemAddedEvent += AddItemController;
        }

        public ItemController RemoveItemController(ItemType itemType, int amount)
        {
            if (!Inventory.HasItems(itemType))
                return null;

            var itemController = _itemControllers.First(x => x.ItemType == itemType);

            itemController.transform.parent = null;
            _itemControllers.RemoveAt(0);

            return itemController;
        }

        private void AddItemController(ItemType itemType, int amount)
        {
            if (_itemControllers == null)
                _itemControllers = new List<ItemController>();

            var itemController = FactoriesController.Instance.ItemFactory.GetInstance(itemType);

            itemController.transform.localScale = Vector3.zero;

            itemController.transform.SetParent(ItemsInstanceParent);
            itemController.gameObject.SetActive(true);
            itemController.transform.DOScale(Vector3.one, 0.1f) // TO DO: set other size
                .SetEasing(Ease.Type.BackOut);

            _itemControllers.Add(itemController);
        }

        private void OnDestroy()
        {
            if (Inventory == null)
                return;

            Inventory.OnItemAddedEvent -= AddItemController;
        }
    }
}