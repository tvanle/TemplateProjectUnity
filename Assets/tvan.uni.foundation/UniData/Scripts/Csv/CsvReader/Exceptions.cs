namespace tvan.uni.foundation.UniData.Scripts.Csv.CsvReader
{
    using System;

    public class FieldDontExistInBlueprint : Exception
    {
        public FieldDontExistInBlueprint(string message) : base(message) { }
    }
}