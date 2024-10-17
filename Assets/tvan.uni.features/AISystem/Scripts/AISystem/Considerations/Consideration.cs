namespace tvan.uni.features.AISystem.Scripts.AISystem.Considerations
{
    using tvan.uni.features.AISystem.Scripts.AISystem.Brain;
    using UnityEngine;

    public abstract class Consideration : ScriptableObject
    {
        public abstract float Evaluate(Context context);
    }
}