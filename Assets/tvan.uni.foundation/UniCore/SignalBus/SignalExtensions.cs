namespace tvan.uni.foundation.UniCore.SignalBus
{
    using MessagePipe;
    using Zenject;

    public static class SignalExtensions
    {
        private static readonly MessagePipeOptions Options = new();

        public static void DeclareSignal<TSignal>(this DiContainer container) { container.BindMessageBroker<TSignal>(Options); }
    }
}