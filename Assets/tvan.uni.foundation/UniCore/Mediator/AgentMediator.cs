namespace tvan.uni.foundation.UniCore.Mediator
{
    using UnityEngine;

    public class AgentMediator : Mediator<Agent>
    {
        protected override bool MediatorConditionMet(Agent target) { return target.Status == AgentStatus.Active; }

        protected override void OnRegistered(Agent entity)
        {
            Debug.Log($"{entity.name} registered");
            this.Broadcast(entity, new MessagePayload { Source = entity, Content = "Registered" });
        }

        protected override void OnDeregistered(Agent entity)
        {
            Debug.Log($"{entity.name} deregistered");
            this.Broadcast(entity, new MessagePayload { Source = entity, Content = "Deregistered" });
        }
    }
}