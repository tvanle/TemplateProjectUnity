namespace tvan.uni.foundation.UniData.Scripts.LocalData
{
    using System;
    using tvan.uni.foundation.UniCore.Extension;
    using tvan.uni.foundation.UniCore.SignalBus;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Interface;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Manager;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Service;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Signal;
    using Zenject;

    public class UniLocalDataInstaller : Installer<UniLocalDataInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.Bind<ILocalDataServices>().To<LocalLocalDataServices>().AsCached();
            this.Container.DeclareSignal<LocalDataLoadedSignal>();

            this.BindLocalData();
        }

        private void BindLocalData()
        {
            ReflectionUtils.GetAllDerivedTypes<ILocalData>().ForEach(type =>
            {
                var data = Activator.CreateInstance(type);
                this.Container.Bind(type).FromInstance(data).AsCached();
                var controllerType = ((ILocalData)data).ControllerType;
                this.Container.BindInterfacesAndSelfTo(controllerType).AsCached();
                var controller = this.Container.Resolve(controllerType) as ILocalDataController;
                controller?.Initialize((ILocalData)data);
            });

            this.Container.Bind<LocalDataManager>().AsCached().NonLazy();
        }

        private void BindAllController()
        {
            var listController = ReflectionUtils.GetAllDerivedTypes<ILocalDataController>();

            foreach (var type in listController)
            {
                var controller = Activator.CreateInstance(type);
                this.Container.Bind(type).FromInstance(controller).AsCached();
            }
        }
    }
}