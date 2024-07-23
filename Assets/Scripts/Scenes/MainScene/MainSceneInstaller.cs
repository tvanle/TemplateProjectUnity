namespace Scenes.MainScene
{
    using GDK_TrongLe.UniUI.Scripts.Manager;
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