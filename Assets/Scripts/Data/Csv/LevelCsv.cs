namespace Data.Csv
{
    using Tvan.Foundation.UniData.Scripts.Csv.CsvReader;

    [CsvInfo("Level")]
    public class LevelCsv : GenericCsvReader<int, LevelRecord>
    {
    }

    [Key("ID")]
    public class LevelRecord
    {
        public int                        ID              { get; set; }
        public string                     GameType        { get; set; }
        public int                        GameTypeLevelId { get; set; }
        public CsvReader<int, BoolRecord> BoolBlueprint   { get; set; }
    }

    [Key("BoolId")]
    public class BoolRecord
    {
        public bool Boolean { get; set; }
        public int  BoolId  { get; set; }
    }
}