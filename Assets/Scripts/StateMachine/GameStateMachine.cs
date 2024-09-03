namespace StateMachine
{
    using System.Collections.Generic;
    using StateMachine.States;
    using tvan.uni.foundation.UniCore.SignalBus;
    using tvan.uni.foundation.UniCore.StateMachine.Controller;
    using tvan.uni.foundation.UniCore.StateMachine.Interface;
    using Zenject;

    public class GameStateMachine : StateMachine, IInitializable
    {
        public GameStateMachine(List<IState> listGameState, SignalBus signalBus) : base(listGameState, signalBus)
        {
            listGameState.ForEach(e =>
            {
                if (e is BaseGameState state) state.GameStateMachine = this;
            });
        }

        public void Initialize() { }
    }
}