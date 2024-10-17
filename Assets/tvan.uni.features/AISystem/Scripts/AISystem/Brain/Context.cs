namespace tvan.uni.features.AISystem.Scripts.AISystem.Brain
{
    using System.Collections.Generic;
    using AudioSystem;
    using UnityEngine;
    using UnityEngine.AI;

    public class Context
    {
        public Brain        brain;
        public NavMeshAgent agent;
        public Transform    target;
        public Sensor       sensor;

        private readonly Dictionary<string, object> data = new();

        public Context(Brain brain)
        {
            Preconditions.CheckNotNull(brain, nameof(brain));

            this.brain  = brain;
            this.agent  = brain.gameObject.GetOrAdd<NavMeshAgent>();
            this.sensor = brain.gameObject.GetOrAdd<Sensor>();
        }

        public T GetData<T>(string key) { return this.data.TryGetValue(key, out var value) ? (T)value : default; }

        public void SetData(string key, object value) { this.data[key] = value; }
    }
}