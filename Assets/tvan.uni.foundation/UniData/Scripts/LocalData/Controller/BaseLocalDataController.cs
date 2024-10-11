namespace tvan.uni.foundation.UniData.Scripts.LocalData.Controller
{
    using tvan.uni.foundation.UniData.Scripts.LocalData.Interface;

    public abstract class BaseLocalDataController<TData> : ILocalDataController where TData : ILocalData
    {
        protected TData Data { get; private set; }

        public void Initialize(ILocalData localData)
        {
            if (localData is not TData data) return;
            this.Data = data;
            this.OnLoaded();
        }

        public virtual void OnLoaded() { }

        public abstract void FirstTimeOneLoad();
    }
}