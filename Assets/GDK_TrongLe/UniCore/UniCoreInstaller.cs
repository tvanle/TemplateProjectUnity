namespace UniCore.Installer
{
    using UniCore.AssetLibrary;
    using Zenject;

    public class UniCoreInstaller : Installer<UniCoreInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.Bind<IGameAssets>().To<GameAssets>().AsCached();
        }
    }
}