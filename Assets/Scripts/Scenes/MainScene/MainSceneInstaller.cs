namespace Scenes.MainScene
{
    using StateMachine;
    using tvan.uni.foundation.UniUI.Scripts.Manager;

    public class MainSceneInstaller : BaseSceneInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            GameStateMachineInstaller.Install(this.Container);
        }
    }
}