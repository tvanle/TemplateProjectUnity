namespace tvan.uni.foundation.UniData.Scripts.LocalData
{
    using System;
    using tvan.uni.foundation.UniCore.Extension;
    using tvan.uni.foundation.UniCore.SignalBus;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Interface;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Manager;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Signal;
    using tvan.uni.foundation.UniData.Scripts.LocalData.UserData;
    using Zenject;

    public class UniLocalDataInstaller : Installer<UniLocalDataInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.Bind<IHandleUserDataServices>().To<HandleUserDataServices>().AsCached();
            this.Container.DeclareSignal<UserDataLoadedSignal>();

            this.BindLocalData();
            this.BindAllController();
        }

        private void BindLocalData()
        {
            ReflectionUtils.GetAllDerivedTypes<ILocalData>().ForEach(type =>
            {
                var data = Activator.CreateInstance(type);
                this.Container.Bind(type).FromInstance(data).AsCached();
            });

            this.Container.Bind<UserDataManager>().AsCached().NonLazy();
        }

        private void BindAllController()
        {
            var listController = ReflectionUtils.GetAllDerivedTypes<ILocalDataController>();

            foreach (var controller in listController) this.Container.BindInterfacesAndSelfTo(controller).AsCached();
        }
    }
}