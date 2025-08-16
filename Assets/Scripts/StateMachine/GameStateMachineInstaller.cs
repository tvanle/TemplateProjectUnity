namespace StateMachine
{
    using StateMachine.Interface;
    using Tvan.Foundation.UniCore.Extension;
    using VContainer;
    using VContainer.Unity;

    public static class GameStateMachineInstaller
    {
        public static void Install(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<GameStateMachine>();

            // Register all game states
            var gameStateTypes = ReflectionUtils.GetAllDerivedTypes<IGameState>();
            foreach (var type in gameStateTypes)
                if (!type.IsAbstract && !type.IsInterface)
                    builder.Register(type, Lifetime.Transient).As<IGameState>();
        }
    }
}