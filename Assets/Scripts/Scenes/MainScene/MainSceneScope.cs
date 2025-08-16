namespace Scenes.MainScene
{
    using GameFoundation.Scripts.UIModule.Utilities.LoadImage;
    using StateMachine;
    using Tvan.Foundation.DI;
    using VContainer;

    public class MainSceneScope : SceneScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            GameStateMachineInstaller.Install(builder);
            builder.Register<LoadImageHelper>(Lifetime.Singleton);
        }
    }
}