namespace tvan.uni.foundation.UniCore.StateMachine.Signal
{
    using tvan.uni.foundation.UniCore.StateMachine.Interface;

    public class OnStateEnterSignal
    {
        
        public IState State { get; }

        public OnStateEnterSignal(IState state) { this.State = state; }
    }
}