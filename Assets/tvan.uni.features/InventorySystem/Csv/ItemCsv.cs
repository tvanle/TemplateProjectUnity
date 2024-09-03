namespace tvan.uni.features.InventorySystem.Csv
{
    using tvan.uni.foundation.UniData.Scripts.Csv.CsvReader;

    [CsvInfo("Item")]
    public class ItemCsv : GenericCsvReader<string, ItemRecord>
    {
    }

    [Key("Id")]
    public class ItemRecord
    {
        public string Id            { get; set; }
        public string Name          { get; set; }
        public string Description   { get; set; }
        public string ImageAddress  { get; set; }
        public string Category      { get; set; }
        public bool   IsDefaultItem { get; set; }
    }
}