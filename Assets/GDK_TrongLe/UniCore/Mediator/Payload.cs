namespace GDK_TrongLe.UniCore.Mediator
{
    using UnityEngine;

    public abstract class Payload<TData> : IVisitor
    {
        public abstract TData Content { get; set; }

        public abstract void Visit<T>(T visitable) where T : Component, IVisitable;
    }

    public class MessagePayload : Payload<string>
    {
        public          Agent  Source  { get; set; }
        public override string Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            Debug.Log($"{visitable.name} received message: {this.Content} from {this.Source.name}");
            //Execute logic on here
        }
    }
}