using Core.Inventory;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "ItemSettings", menuName = "Settings/Inventory/ItemSettings", order = 52)]
    public sealed class ItemSettings : ScriptableObject
    {
        [field: SerializeField] public ItemType Type { get; private set; }
    }
}