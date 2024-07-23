namespace GDK_TrongLe.UniCore
{
    using GDK_TrongLe.UniCore.AssetLibrary.Scripts;
    using GDK_TrongLe.UniCore.SignalBus;
    using Zenject;

    public class UniCoreInstaller : Installer<UniCoreInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.Bind<IGameAssets>().To<GameAssets>().AsCached().NonLazy();
            SignalBusInstaller.Install(this.Container);
        }
    }
}