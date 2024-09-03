namespace tvan.uni.foundation.UniData.Scripts.LocalData.Manager
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Cysharp.Threading.Tasks;
    using tvan.uni.foundation.UniCore.Extension;
    using tvan.uni.foundation.UniCore.SignalBus;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Interface;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Signal;
    using tvan.uni.foundation.UniData.Scripts.LocalData.UserData;
    using Zenject;

    public class UserDataManager
    {
        private readonly DiContainer             container;
        private readonly SignalBus               signalBus;
        private readonly IHandleUserDataServices handleUserDataService;

        public UserDataManager(DiContainer container, SignalBus signalBus, IHandleUserDataServices handleUserDataService)
        {
            this.container             = container;
            this.signalBus             = signalBus;
            this.handleUserDataService = handleUserDataService;
        }

        public async UniTask LoadUserData()
        {
            var types = ReflectionUtils.GetAllDerivedTypes<ILocalData>().ToArray();
            var datas = await this.handleUserDataService.Load(types);
            var datasCache = (Dictionary<string, ILocalData>)typeof(HandleUserDataServices)
                .GetField("userDataCache", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(this.handleUserDataService);
            IterTools.Zip(types, datas).ForEach((type, data) =>
            {
                var boundData = this.container.Resolve(type);

                data.CopyTo(boundData);
                if (datasCache != null) datasCache[HandleUserDataServices.KeyOf(boundData.GetType())] = boundData as ILocalData;
            });
            this.signalBus.Fire<UserDataLoadedSignal>();
        }
    }
}