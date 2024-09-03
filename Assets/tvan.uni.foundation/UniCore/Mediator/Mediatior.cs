namespace tvan.uni.foundation.UniCore.Mediator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using tvan.uni.foundation.UniCore.Extension;
    using UnityEngine;

    public abstract class Mediator<T> : MonoBehaviour where T : Component, IVisitable
    {
        private readonly List<T> entities = new();

        public void Register(T entity)
        {
            if (!this.entities.Contains(entity))
            {
                this.entities.Add(entity);
                this.OnRegistered(entity);
            }
        }

        protected virtual void OnRegistered(T entity)
        {
            // noop
        }

        public void Deregister(T entity)
        {
            if (this.entities.Contains(entity))
            {
                this.entities.Remove(entity);
                this.OnDeregistered(entity);
            }
        }

        protected virtual void OnDeregistered(T entity)
        {
            // noop
        }

        public void Message(T source, T target, IVisitor message) { this.entities.FirstOrDefault(entity => entity.Equals(target))?.Accept(message); }

        public void Broadcast(T source, IVisitor message, Func<T, bool> predicate = null)
        {
            this.entities.Where(target => source != target && this.SenderConditionMet(target, predicate) && this.MediatorConditionMet(target))
                .ForEach(target => target.Accept(message));
        }

        private bool SenderConditionMet(T target, Func<T, bool> predicate) { return predicate == null || predicate(target); }

        protected abstract bool MediatorConditionMet(T target);
    }
}