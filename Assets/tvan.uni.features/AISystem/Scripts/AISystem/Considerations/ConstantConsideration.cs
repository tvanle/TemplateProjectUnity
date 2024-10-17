namespace tvan.uni.features.AISystem.Scripts.AISystem.Considerations
{
    using tvan.uni.features.AISystem.Scripts.AISystem.Brain;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UtilityAI/Considerations/Constant")]
    public class ConstantConsideration : Consideration
    {
        public float value;

        public override float Evaluate(Context context) { return this.value; }
    }
}