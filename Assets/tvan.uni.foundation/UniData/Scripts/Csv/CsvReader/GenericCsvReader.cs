namespace tvan.uni.foundation.UniData.Scripts.Csv.CsvReader
{
    using System.Collections.Generic;
    using System.IO;
    using Cysharp.Threading.Tasks;
    using Sylvan.Data.Csv;
    using tvan.uni.foundation.UniData.Scripts.Csv.CsvReader.Converter;

    /// <summary>
    ///     An abstraction class for databases with row-based header fields
    /// </summary>
    /// <typeparam name="T1">Type of header key</typeparam>
    /// <typeparam name="T2">Type of value</typeparam>
    public abstract class GenericCsvReader<T1, T2> : CsvReader<T1, T2>, IGenericCsvReader
    {
        public virtual async UniTask DeserializeFromCsv(string rawCsv)
        {
            this.CleanUp();
            await using var csv =
                await CsvDataReader.CreateAsync(new StringReader(rawCsv), CsvHelper.CsvDataReaderOptions);
            while (await csv.ReadAsync()) this.Add(csv);
        }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TRecord"></typeparam>
    public class CsvReader<TKey, TRecord> : Dictionary<TKey, TRecord>, ICsvCollection
    {
        private readonly CsvRecordReader<TRecord> csvRecordReader;

        // Need to be public due to reflection construction
        public CsvReader() { this.csvRecordReader = new CsvRecordReader<TRecord>(this.GetType()); }

        public void Add(CsvDataReader inputCsv)
        {
            var (hasValue, record) = this.csvRecordReader.IsGetRecord(inputCsv);
            if (hasValue) this.Add(inputCsv.GetField<TKey>(this.csvRecordReader.RequireKey), record);
        }

        public void CleanUp() { this.Clear(); }
    }
}