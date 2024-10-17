namespace UtilityAI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent), typeof(Sensor))]
    public class Brain : MonoBehaviour
    {
        public List<AIAction> actions;
        public Context        context;

        public Health health;

        private void Awake()
        {
            this.context = new Context(this);
            this.health  = this.GetComponent<Health>();

            foreach (var action in this.actions) action.Initialize(this.context);
        }

        private void Update()
        {
            this.UpdateContext();

            AIAction bestAction     = null;
            var      highestUtility = float.MinValue;

            foreach (var action in this.actions)
            {
                var utility = action.CalculateUtility(this.context);
                if (utility > highestUtility)
                {
                    highestUtility = utility;
                    bestAction     = action;
                }
            }

            if (bestAction != null) bestAction.Execute(this.context);
        }

        private void UpdateContext() { this.context.SetData("health", this.health.normalizedHealth); }
    }
}