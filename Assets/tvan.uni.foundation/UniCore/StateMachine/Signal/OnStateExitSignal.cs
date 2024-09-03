namespace tvan.uni.foundation.UniCore.StateMachine.Signal
{
    using tvan.uni.foundation.UniCore.StateMachine.Interface;

    public class OnStateExitSignal
    {
        public IState State { get; }

        public OnStateExitSignal(IState state) { this.State = state; }
    }
}