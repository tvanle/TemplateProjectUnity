namespace GDK_TrongLe.UniCore
{
    using GDK_TrongLe.UniCore.AssetLibrary.Scripts;
    using Zenject;

    public class UniCoreInstaller : Installer<UniCoreInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.Bind<IGameAssets>().To<GameAssets>().AsCached();
        }
    }
}