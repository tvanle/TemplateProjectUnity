namespace GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintReader.ReaderByRow
{
    using System.Collections.Generic;
    using Sylvan.Data.Csv;

    public interface IBlueprintCollection
    {
        void Add(CsvDataReader inputCsv);

        List<List<string>> ToRawData(bool containHeader = false);

        void CleanUp();
    }
}