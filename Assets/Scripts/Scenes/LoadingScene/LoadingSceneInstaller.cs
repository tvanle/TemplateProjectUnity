namespace Scenes.LoadingScene
{
    using GDK_TrongLe.UniUI.Scripts.Demos;
    using GDK_TrongLe.UniUI.Scripts.Manager;

    public class LoadingSceneInstaller : BaseSceneInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            this.screenManager.OpenScreen<LoadingScreenPresenter>();
        }
    }
}