namespace GDK_TrongLe.UniData.Scripts
{
    using System;
    using GDK_TrongLe.UniCore.Extension;
    using GDK_TrongLe.UniData.Scripts.Interface;
    using GDK_TrongLe.UniData.Scripts.Manager;
    using GDK_TrongLe.UniData.Scripts.Signal;
    using GDK_TrongLe.UniData.Scripts.UserData;
    using Zenject;

    public class UniDataInstaller : Installer<UniDataInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.Bind<IHandleUserDataServices>().To<HandleLocalUserDataServices>().AsCached();
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

            this.Container.Bind<UserDataManager>().AsCached();
        }

        private void BindAllController()
        {
            var listController = ReflectionUtils.GetAllDerivedTypes<ILocalDataController>();

            foreach (var controller in listController) this.Container.BindInterfacesAndSelfTo(controller).AsCached();
        }
    }
}