namespace Data.Blueprint
{
    using GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintReader;
    using GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintReader.ReaderByRow;

    [BlueprintReader("Level")]
    [CsvHeaderKey("ID")]
    public class LevelBlueprint : GenericBlueprintReaderByRow<int, LevelRecord>
    {
    }

    public class LevelRecord
    {
        public int    ID              { get; set; }
        public string GameType        { get; set; }
        public int    GameTypeLevelId { get; set; }
    }
}