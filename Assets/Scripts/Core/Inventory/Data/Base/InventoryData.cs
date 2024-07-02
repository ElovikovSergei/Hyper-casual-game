using System.Collections.Generic;
using System;

namespace Core.Inventory
{
    [Serializable]
    public abstract class InventoryData
    {
        public string OwnerId;
        public List<InventorySlotData> SlotsData;
    }
}