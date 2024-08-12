namespace GDK_TrongLe.UniData.Scripts.Csv.CsvReader
{
    using System;

    /// <summary> attributes to store basic information of a blueprint </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CsvInfoAttribute : Attribute
    {
        public string DataPath { get; }
        public CsvInfoAttribute(string dataPath) { this.DataPath = dataPath; }
    }

    /// <summary> Attribute used to mark the Header Key for GenericDatabaseByRow </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Struct)]
    public class KeyAttribute : Attribute
    {
        public readonly string HeaderKey;

        public KeyAttribute(string headerKey) { this.HeaderKey = headerKey; }
    }
}