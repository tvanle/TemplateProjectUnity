namespace tvan.uni.foundation.UniData.Scripts.LocalData.Interface
{
    public interface ILocalDataController
    {
        void Initialize(ILocalData localData);

        void FirstTimeOneLoad();
    }
}