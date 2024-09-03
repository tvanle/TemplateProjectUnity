namespace tvan.uni.features.InventorySystem.Csv
{
    using tvan.uni.features.InventorySystem.Model.Element;
    using tvan.uni.foundation.UniData.Scripts.Csv.CsvReader;

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