using Core.Inventory;
using UnityEngine;

namespace UI.Inventory
{
    [RequireComponent(typeof(InventorySlotView))]
    public sealed class InventorySlotController : MonoBehaviour
    {
        [SerializeField] private InventorySlotView _view;
        private IInventorySlot _slot;

        public void Setup(IInventorySlot slot)
        {
            _slot = slot;

            _slot.OnItemTypeChangedEvent += OnItemTypeChanged;
            _slot.OnItemAmountChangedEvent += OnItemAmountChanged;

            //_view.SetIcon();
            //_view.SetAmount(slot.Amount.ToString());
        }

        private void OnItemAmountChanged(int amount)
        {
            _view.SetAmount(amount.ToString());
        }

        private void OnItemTypeChanged(ItemType itemType)
        {
            //_view.SetIcon();
            // TO DO: getting icon by type
        }

        private void OnDisable()
        {
            _slot.OnItemTypeChangedEvent -= OnItemTypeChanged;
            _slot.OnItemAmountChangedEvent -= OnItemAmountChanged;

            _slot = null;
        }
    }
}