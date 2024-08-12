namespace GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintReader.ReaderByRow
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Cysharp.Threading.Tasks;
    using DataManager.Blueprint.BlueprintReader.Converter;
    using Sylvan.Data.Csv;

    /// <summary>
    ///     An abstraction class for databases with row-based header fields
    /// </summary>
    /// <typeparam name="T1">Type of header key</typeparam>
    /// <typeparam name="T2">Type of value</typeparam>
    public abstract class GenericBlueprintReaderByRow<T1, T2> : BlueprintByRow<T1, T2>, IGenericBlueprintReader
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
    public class BlueprintByRow<TKey, TRecord> : Dictionary<TKey, TRecord>, IBlueprintCollection
    {
        private readonly BlueprintRecordReader<TRecord> blueprintRecordReader;

        // Need to be public due to reflection construction
        public BlueprintByRow() { this.blueprintRecordReader = new BlueprintRecordReader<TRecord>(this.GetType()); }

        public void Add(CsvDataReader inputCsv)
        {
            var (hasValue, record) = this.blueprintRecordReader.GetRecord(inputCsv);
            if (hasValue) this.Add(inputCsv.GetField<TKey>(this.blueprintRecordReader.RequireKey), record);
        }

        // public List<List<string>> ToRawData(bool containHeader = false)
        // {
        //     var result    = new List<List<string>>();
        //     var addHeader = containHeader;
        //     foreach (var record in this)
        //     {
        //         result.AddRange(this.blueprintRecordReader.ToRawData(record.Value, addHeader));
        //         addHeader = false;
        //     }
        //
        //     return result;
        // }

        public void CleanUp() { this.Clear(); }
    }

    // Need to be public due to reflection construction
    // [Serializable]
    // public class BlueprintByRow<TRecord> : List<TRecord>, IBlueprintCollection
    // {
    //     private readonly BlueprintRecordReader<TRecord> blueprintRecordReader;
    //
    //     // Need to be public due to reflection construction
    //     public BlueprintByRow() { this.blueprintRecordReader = new BlueprintRecordReader<TRecord>(this.GetType()); }
    //
    //     public void Add(CsvDataReader inputCsv)
    //     {
    //         var (hasValue, value) = this.blueprintRecordReader.GetRecord(inputCsv);
    //         if (hasValue) this.Add(value);
    //     }
    //
    //     public List<List<string>> ToRawData(bool containHeader = false)
    //     {
    //         var result    = new List<List<string>>();
    //         var addHeader = containHeader;
    //         foreach (var record in this)
    //         {
    //             result.AddRange(this.blueprintRecordReader.ToRawData(record, addHeader));
    //             addHeader = false;
    //         }
    //
    //         return result;
    //     }
    //
    //     public void CleanUp() { this.Clear(); }
    // }

    public class BlueprintRecordReader<TRecord> : BlueprintRecordReader
    {
        public BlueprintRecordReader(Type blueprintType) : base(blueprintType, typeof(TRecord)) { }

        public new (bool, TRecord) GetRecord(CsvDataReader inputCsv)
        {
            var record = base.GetRecord(inputCsv);

            return record != null ? (true, (TRecord)record) : (false, default);
        }
    }
}