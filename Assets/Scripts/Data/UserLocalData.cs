namespace Data
{
    using GDK_TrongLe.UniData.Scripts.Interface;

    public class UserLocalData : ILocalData
    {
        public int    Id;
        public string Name;

        public void Init()
        {
            this.Id   = 5;
            this.Name = "User";
        }
    }
}