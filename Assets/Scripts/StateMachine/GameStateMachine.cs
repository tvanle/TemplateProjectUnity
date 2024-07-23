namespace StateMachine
{
    using System.Collections.Generic;
    using GDK_TrongLe.UniCore.SignalBus;
    using GDK_TrongLe.UniCore.StateMachine.Controller;
    using GDK_TrongLe.UniCore.StateMachine.Interface;
    using StateMachine.States;
    using UnityEngine;
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

        public void Initialize() { Debug.LogError("12"); }
    }
}