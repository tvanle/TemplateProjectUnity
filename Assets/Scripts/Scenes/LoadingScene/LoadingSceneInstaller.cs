namespace Scenes.LoadingScene
{
    using tvan.uni.foundation.UniUI.Scripts.Manager;
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