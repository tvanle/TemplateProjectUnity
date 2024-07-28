namespace Data
{
    using GDK_TrongLe.UniData.Scripts.Interface;

    public class UserLocalData : ILocalData
    {
        public int    Id   { get; set; }
        public string Name { get; set; }

        public void Init()
        {
            this.Id   = 1;
            this.Name = "Trong Le";
        }
    }
}