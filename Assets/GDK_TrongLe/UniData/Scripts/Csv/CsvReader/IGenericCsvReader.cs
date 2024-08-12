namespace GDK_TrongLe.UniData.Scripts.Csv.CsvReader
{
    using Cysharp.Threading.Tasks;
    using Sylvan.Data.Csv;

    /// <summary> Interface of database class </summary>
    public interface IGenericCsvReader
    {
        /// <summary>
        ///     Auto binding data from the raw Csv file to properties of database
        /// </summary>
        /// <param name="rawCsv"></param>
        /// <returns></returns>
        public UniTask DeserializeFromCsv(string rawCsv);
    }

    public interface ICsvCollection
    {
        void Add(CsvDataReader inputCsv);
        void CleanUp();
    }
}