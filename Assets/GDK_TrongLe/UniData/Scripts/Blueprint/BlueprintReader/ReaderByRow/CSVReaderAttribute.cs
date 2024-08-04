namespace GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintReader.ReaderByRow
{
    using System;

    /// <summary> Attribute used to mark the Header Key for GenericDatabaseByRow </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Struct)]
    public class CsvHeaderKeyAttribute : Attribute
    {
        public readonly string HeaderKey;

        public CsvHeaderKeyAttribute(string headerKey) { this.HeaderKey = headerKey; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class NestedBlueprintAttribute : Attribute
    {
    }
}