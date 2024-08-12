namespace GDK_TrongLe.UniCore.StateMachine.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GDK_TrongLe.UniCore.SignalBus;
    using GDK_TrongLe.UniCore.StateMachine.Interface;
    using GDK_TrongLe.UniCore.StateMachine.Signal;
    using UnityEngine;
    using Zenject;

    public abstract class StateMachine : IStateMachine, ITickable
    {
        #region inject

        protected readonly SignalBus                SignalBus;
        protected readonly Dictionary<Type, IState> TypeToState;

        #endregion

        protected StateMachine(
            List<IState> listState,
            SignalBus signalBus
        )
        {
            this.SignalBus   = signalBus;
            this.TypeToState = listState.ToDictionary(state => state.GetType(), state => state);
        }

        public IState CurrentState { get; private set; }

        public void TransitionTo<T>() where T : class, IState { this.TransitionTo(typeof(T)); }

        public void TransitionTo<TState, TModel>(TModel model) where TState : class, IState<TModel>
        {
            var stateType = typeof(TState);

            if (!this.TypeToState.TryGetValue(stateType, out var nextState)) return;

            if (nextState is not TState nextStateT) return;
            nextStateT.Model = model;

            this.InternalStateTransition(nextState);
        }

        public virtual void TransitionTo(Type stateType)
        {
            if (!this.TypeToState.TryGetValue(stateType, out var nextState)) return;

            this.InternalStateTransition(nextState);
        }

        private void InternalStateTransition(IState nextState)
        {
            if (this.CurrentState != null)
            {
                this.CurrentState.Exit();
                this.SignalBus.Fire(new OnStateExitSignal(this.CurrentState));
                Debug.Log($"Exit {this.CurrentState.GetType().Name} State!!!");
            }

            this.CurrentState = nextState;
            this.SignalBus.Fire(new OnStateEnterSignal(this.CurrentState));
            Debug.Log($"Enter {nextState.GetType().Name} State!!!");
            nextState.Enter();
        }

        public void Tick()
        {
            (this.CurrentState as ITickable)?.Tick();
        }
    }
}