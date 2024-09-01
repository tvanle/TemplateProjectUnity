namespace GameFeature.InventorySystem.Model.LocalData
{
    using System.Collections.Generic;
    using GameFeature.InventorySystem.Model.Element;
    using GDK_TrongLe.UniData.Scripts.LocalData.Interface;

    public class InventoryData : ILocalData
    {
        public Dictionary<string, ItemData> CategoryToChosenItemData = new();
        public Dictionary<string, ItemData> IdToItemData             = new();

        public void Init() { }
    }
}