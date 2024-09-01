namespace GDK_TrongLe.UniCore.Mediator
{
    using UnityEngine;

    public interface IVisitor
    {
        void Visit<T>(T visitable) where T : Component, IVisitable;
    }

    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}