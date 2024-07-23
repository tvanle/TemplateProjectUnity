namespace GDK_TrongLe.UniUI.Scripts
{
    using GDK_TrongLe.UniUI.Scripts.Manager;
    using Zenject;

    public class UniUIInstaller : Installer<UniUIInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<ScreenManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}