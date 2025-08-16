namespace StateMachine
{
    using System.Collections.Generic;
    using StateMachine.States;
    using Tvan.Foundation.UniCore.SignalBus;
    using Tvan.Foundation.UniCore.StateMachine.Controller;
    using Tvan.Foundation.UniCore.StateMachine.Interface;
    using VContainer.Unity;

    public class GameStateMachine : StateMachine, IStartable
    {
        public GameStateMachine(IEnumerable<IState> listGameState, SignalBus signalBus) : base(new(listGameState), signalBus)
        {
            foreach (var state in listGameState)
            {
                if (state is BaseGameState baseState) baseState.GameStateMachine = this;
            }
        }

        public void Start() { }
    }
}