namespace GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintReader
{
    using System;

    public class FieldDontExistInBlueprint : Exception
    {
        public FieldDontExistInBlueprint(string message) : base(message) { }
    }
}