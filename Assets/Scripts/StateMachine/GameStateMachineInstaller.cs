namespace StateMachine
{
    using StateMachine.Interface;
    using Zenject;

    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<GameStateMachine>().AsCached().NonLazy();
            this.Container.Bind<IGameState>().To(convention => convention.AllNonAbstractClasses().DerivingFrom<IGameState>()).WhenInjectedInto<GameStateMachine>();
        }
    }
}