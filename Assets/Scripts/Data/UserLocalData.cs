namespace Data
{
    using System;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Interface;

    public class UserLocalData : ILocalData
    {
        public int    Id;
        public string Name;

        public void Init()
        {
            this.Id   = 5;
            this.Name = "User";
        }

        public Type ControllerType { get; } = typeof(UserLocalDataController);
    }
}