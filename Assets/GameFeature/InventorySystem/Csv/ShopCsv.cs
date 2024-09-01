namespace GameFeature.InventorySystem.Csv
{
    using GameFeature.InventorySystem.Model.Element;
    using GDK_TrongLe.UniData.Scripts.Csv.CsvReader;

    [CsvInfo("Shop")]
    public class ShopCsv : GenericCsvReader<string, ShopRecord>
    {
    }

    [Key("Id")]
    public class ShopRecord
    {
        public string              Id         { get; set; }
        public ItemData.UnlockType UnlockType { get; set; }
        public string              CurrencyID { get; set; }
        public int                 Price      { get; set; }
    }
}