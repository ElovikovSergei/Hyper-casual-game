using Core.Inventory;
using UnityEngine;

namespace UI.Inventory
{
    [RequireComponent(typeof(InventorySlotView))]
    public sealed class InventorySlotController : MonoBehaviour
    {
        [SerializeField] private InventorySlotView _view;

        public void Setup(IInventorySlot slot)
        {
            slot.OnItemTypeChangedEvent += OnItemTypeChanged;
            slot.OnItemAmountChangedEvent += OnItemAmountChanged;

            //_view.SetIcon();
            //_view.SetAmount(slot.Amount.ToString());
        }

        private void OnItemAmountChanged(int amount)
        {
            _view.SetAmount(amount.ToString());
        }

        private void OnItemTypeChanged(ItemType itemType)
        {
            // TO DO: getting icon by type
        }
    }
}