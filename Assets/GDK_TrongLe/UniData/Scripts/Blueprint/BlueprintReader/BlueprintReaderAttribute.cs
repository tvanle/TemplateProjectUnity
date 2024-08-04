namespace GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintReader
{
    using System;

    /// <summary> attributes to store basic information of a blueprint </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BlueprintReaderAttribute : Attribute
    {
        public BlueprintReaderAttribute(string dataPath)
        {
            this.DataPath           = dataPath;
        }

        public string         DataPath           { get; }
    }
    
}