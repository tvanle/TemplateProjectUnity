namespace Scenes.LoadingScene
{
    using GDK_TrongLe.UniUI.Scripts.Manager;
    using UI.Loading;

    public class LoadingSceneInstaller : BaseSceneInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            this.screenManager.OpenScreen<LoadingScreenPresenter>();
        }
    }
}