namespace GameFeature.InventorySystem.Model.Element
{
    using System;
    using System.Collections.Generic;
    using GameFeature.InventorySystem.Csv;

    public class ItemData
    {
        public static readonly DefaultComparer DefaultComparerInstance = new();

        public string     Id                   { get; set; }
        public Status     CurrentStatus        { get; set; }
        public int        RemainingAdsProgress { get; set; }
        public ShopRecord ShopBlueprintRecord  { get; set; }
        public ItemRecord ItemBlueprintRecord  { get; set; }

        public ItemData(string id,
                        ShopRecord shopBlueprintRecord,
                        ItemRecord itemBlueprintRecord,
                        Status currentStatus = Status.Locked)
        {
            this.Id                  = id;
            this.CurrentStatus       = currentStatus;
            this.ShopBlueprintRecord = shopBlueprintRecord;
            this.ItemBlueprintRecord = itemBlueprintRecord;

            if (this.ShopBlueprintRecord is not null && (this.ShopBlueprintRecord.UnlockType & UnlockType.Ads) != 0) this.RemainingAdsProgress = this.ShopBlueprintRecord?.Price ?? 0;
        }

        public enum Status
        {
            Owned      = 0,
            Unlocked   = 1,
            InProgress = 2,
            Locked     = 3
        }

        [Flags]
        public enum UnlockType
        {
            None         = 0,
            IAP          = 1 << 1,
            SoftCurrency = 1 << 2,
            Ads          = 1 << 3,
            Progression  = 1 << 4,
            Gift         = 1 << 5,
            DailyReward  = 1 << 6,
            LuckySpin    = 1 << 7,
            StartedPack  = 1 << 8,
            Shard        = 1 << 9,
            All          = -1,
            Default      = IAP | SoftCurrency | Ads | Progression | Gift | StartedPack
        }

        public class DefaultComparer : IComparer<ItemData>
        {
            public int Compare(ItemData x, ItemData y)
            {
                //Check ref and null first
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;

                //Check status first
                var currentStatusComparison = x.CurrentStatus.CompareTo(y.CurrentStatus);

                if (currentStatusComparison != 0) return currentStatusComparison;

                //If status is equal, then check progress
                var progressComparison = x.RemainingAdsProgress.CompareTo(y.RemainingAdsProgress);

                if (progressComparison != 0) return progressComparison;

                //if progress is equal, then check id
                return string.Compare(x.Id, y.Id, StringComparison.Ordinal);
            }
        }
    }
}