namespace tvan.uni.foundation.UniCore
{
    using tvan.uni.foundation.UniCore.AssetLibrary.Scripts;
    using tvan.uni.foundation.UniCore.SignalBus;
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