using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.Inventory
{
    public sealed class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _amountLabel;
        [SerializeField] private Image _icon;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetAmount(string amount)
        {
            _amountLabel.text = amount;
        }
    }
}