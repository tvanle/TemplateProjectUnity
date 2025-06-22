namespace Scenes.MainScene
{
    using GameFoundation.Scripts.UIModule.Utilities.LoadImage;
    using Packages.Tvan.Foundation.UniUI.Scripts.Manager;
    using StateMachine;

    public class MainSceneInstaller : BaseSceneInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            GameStateMachineInstaller.Install(this.Container);
            this.Container.Bind<LoadImageHelper>().AsSingle();
        }
    }
}