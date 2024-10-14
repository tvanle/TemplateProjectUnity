namespace tvan.uni.features.InventorySystem.Model.LocalData
{
    using System;
    using System.Collections.Generic;
    using tvan.uni.features.InventorySystem.Model.Controller;
    using tvan.uni.features.InventorySystem.Model.Element;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Interface;

    public class InventoryData : ILocalData
    {
        public Dictionary<string, ItemData> CategoryToChosenItemData = new();
        public Dictionary<string, ItemData> IdToItemData             = new();

        public void Init() { }

        public Type ControllerType { get; } = typeof(InventoryDataController);
    }
}