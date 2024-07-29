namespace GDK_TrongLe.UniData.Scripts.Manager
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Cysharp.Threading.Tasks;
    using GDK_TrongLe.UniCore.Extension;
    using GDK_TrongLe.UniCore.SignalBus;
    using GDK_TrongLe.UniData.Scripts.Interface;
    using GDK_TrongLe.UniData.Scripts.Signal;
    using GDK_TrongLe.UniData.Scripts.UserData;
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