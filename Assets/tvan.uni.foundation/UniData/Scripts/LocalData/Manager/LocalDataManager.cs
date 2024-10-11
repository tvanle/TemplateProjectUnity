namespace tvan.uni.foundation.UniData.Scripts.LocalData.Manager
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Cysharp.Threading.Tasks;
    using tvan.uni.foundation.UniCore.Extension;
    using tvan.uni.foundation.UniCore.SignalBus;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Interface;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Service;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Signal;
    using Zenject;

    public class LocalDataManager
    {
        private readonly DiContainer        container;
        private readonly SignalBus          signalBus;
        private readonly ILocalDataServices localDataService;

        public LocalDataManager(DiContainer container, SignalBus signalBus, ILocalDataServices localDataService)
        {
            this.container        = container;
            this.signalBus        = signalBus;
            this.localDataService = localDataService;
        }

        public async UniTask LoadUserData()
        {
            var types = ReflectionUtils.GetAllDerivedTypes<ILocalData>().ToArray();
            var dates = await this.localDataService.Load(types);
            var dataCache = (Dictionary<string, ILocalData>)typeof(LocalLocalDataServices)
                                                            .GetField("userDataCache", BindingFlags.Instance | BindingFlags.NonPublic)
                                                            ?.GetValue(this.localDataService);
            IterTools.Zip(types, dates).ForEach((type, data) =>
            {
                var boundData = this.container.Resolve(type);

                data.CopyTo(boundData);
                if (dataCache != null) dataCache[LocalLocalDataServices.KeyOf(boundData.GetType())] = boundData as ILocalData;
            });
            this.signalBus.Fire<LocalDataLoadedSignal>();
        }
    }
}