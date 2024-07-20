namespace GDK_TrongLe.UniCore.StateMachine.Signal
{
    using GDK_TrongLe.UniCore.StateMachine.Interface;

    public class OnStateEnterSignal
    {
        
        public IState State { get; }

        public OnStateEnterSignal(IState state) { this.State = state; }
    }
}