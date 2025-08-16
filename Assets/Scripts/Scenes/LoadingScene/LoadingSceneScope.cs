namespace Scenes.LoadingScene
{
    using Tvan.Foundation.DI;
    using Tvan.Foundation.UniUI.Scripts.Manager;
    using UI.Loading;
    using VContainer;

    public class LoadingSceneScope : SceneScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var screenManager = this.Container.Resolve<ScreenManager>();
            _ = screenManager.OpenScreen<LoadingScreenPresenter, LoadingScreenModel>(new());
        }
    }
}