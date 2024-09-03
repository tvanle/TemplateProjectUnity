namespace tvan.uni.foundation.UniCore.Mediator
{
    using UnityEngine;

    public class Agent : MonoBehaviour, IVisitable
    {
        public Mediator<Agent> meiator;
        public AgentStatus     Status { get; set; } = AgentStatus.Active;

        public void Accept(IVisitor visitor) { }

        private void Awake() { this.meiator.Register(this); }
    }

    public enum AgentStatus
    {
        Active,
        Busy,
        Rest
    }
}