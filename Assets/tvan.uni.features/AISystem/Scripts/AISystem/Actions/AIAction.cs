namespace tvan.uni.features.AISystem.Scripts.AISystem.Actions
{
    using tvan.uni.features.AISystem.Scripts.AISystem.Brain;
    using tvan.uni.features.AISystem.Scripts.AISystem.Considerations;
    using UnityEngine;

    public abstract class AIAction : ScriptableObject
    {
        public string        targetTag;
        public Consideration consideration;

        public virtual void Initialize(Context context)
        {
            // Optional initialization logic
        }

        public float CalculateUtility(Context context) { return this.consideration.Evaluate(context); }

        public abstract void Execute(Context context);
    }
}