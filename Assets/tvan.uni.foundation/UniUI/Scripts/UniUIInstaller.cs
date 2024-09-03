namespace tvan.uni.foundation.UniUI.Scripts
{
    using tvan.uni.foundation.UniUI.Scripts.Manager;
    using Zenject;

    public class UniUIInstaller : Installer<UniUIInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<ScreenManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}