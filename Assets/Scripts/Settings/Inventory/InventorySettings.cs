using System.Collections.Generic;
using Core.Inventory;
using System.Linq;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "InventorySettings", menuName = "Settings/Inventory/InventorySettings", order = 52)]
    public sealed class InventorySettings : ScriptableObject
    {
        [SerializeField] private List<ItemSettings> _itemsSettings;

        private Dictionary<ItemType, ItemSettings> _settings;
        private bool _isInitialized;

#if UNITY_EDITOR
        public void Add(ItemSettings itemSettings)
        {
            _itemsSettings.Add(itemSettings);
        }
#endif

        public ItemSettings GetItemSettings(ItemType type)
        {
            if (!_isInitialized)
                Initialize();

            return _settings[type];
        }

        private void Initialize()
        {
            if (!_itemsSettings.Any())
                return;

            _settings = new Dictionary<ItemType, ItemSettings>();

            _itemsSettings.ForEach(settings =>
            {
                _settings.Add(settings.Type, settings);
            });

            _isInitialized = true;
        }
    }
}