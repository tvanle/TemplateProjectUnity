namespace GDK_TrongLe.UniCore.StateMachine.Signal
{
    using GDK_TrongLe.UniCore.StateMachine.Interface;

    public class OnStateExitSignal
    {
        public IState State { get; }

        public OnStateExitSignal(IState state) { this.State = state; }
    }
}