namespace Scenes.MainScene
{
    using Packages.Tvan.Foundation.UniUI.Scripts.Manager;
    using StateMachine;

    public class MainSceneInstaller : BaseSceneInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            GameStateMachineInstaller.Install(this.Container);
        }
    }
}