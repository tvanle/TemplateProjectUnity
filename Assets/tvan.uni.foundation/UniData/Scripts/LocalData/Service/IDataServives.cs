namespace tvan.uni.foundation.UniData.Scripts.LocalData.Service
{
    using System;
    using Cysharp.Threading.Tasks;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Interface;

    public interface ILocalDataServices
    {
        public UniTask Save<T>(T data, bool force = false) where T : class, ILocalData;

        /// <summary>
        ///     Load data from local
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public UniTask<T> Load<T>() where T : class, ILocalData;

        public UniTask<ILocalData[]> Load(params Type[] types);

        public UniTask SaveAll();
    }
}