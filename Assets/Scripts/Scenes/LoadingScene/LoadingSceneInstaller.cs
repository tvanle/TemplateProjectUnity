namespace Scenes.LoadingScene
{
    using Packages.Tvan.Foundation.UniUI.Scripts.Manager;
    using UI.Loading;

    public class LoadingSceneInstaller : BaseSceneInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            this.ScreenManager.OpenScreen<LoadingScreenPresenter, LoadingScreenModel>(new LoadingScreenModel());
        }
    }
}