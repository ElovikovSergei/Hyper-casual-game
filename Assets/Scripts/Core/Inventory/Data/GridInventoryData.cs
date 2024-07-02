using UnityEngine;
using System;

namespace Core.Inventory
{
    [Serializable]
    public sealed class GridInventoryData : InventoryData
    {
        public Vector2Int Size;
    }
}