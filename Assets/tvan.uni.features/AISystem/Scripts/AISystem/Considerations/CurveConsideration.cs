namespace tvan.uni.features.AISystem.Scripts.AISystem.Considerations
{
    using tvan.uni.features.AISystem.Scripts.AISystem.Brain;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UtilityAI/Considerations/CurveConsideration")]
    public class CurveConsideration : Consideration
    {
        public AnimationCurve curve;
        public string         contextKey;

        public override float Evaluate(Context context)
        {
            var inputValue = context.GetData<float>(this.contextKey);

            var utility = this.curve.Evaluate(inputValue);

            return Mathf.Clamp01(utility);
        }

        private void Reset()
        {
            this.curve = new AnimationCurve(
                new Keyframe(0f, 1f), // At normalized distance 0, utility is 1
                new Keyframe(1f, 0f)  // At normalized distance 1, utility is 0
            );
        }
    }
}