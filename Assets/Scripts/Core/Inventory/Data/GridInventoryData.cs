using System.Collections.Generic;
using UnityEngine;
using System;

namespace Core.Inventory
{
    [Serializable]
    public sealed class GridInventoryData : InventoryData
    {
        public List<InventorySlotData> SlotsData;
        public Vector2Int Size;
    }
}