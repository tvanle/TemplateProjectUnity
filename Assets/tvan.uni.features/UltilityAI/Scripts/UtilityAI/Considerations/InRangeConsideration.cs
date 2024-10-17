namespace UtilityAI
{
    using tvan.uni.foundation.UniCore.Extension.Unity;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UtilityAI/Considerations/InRangeConsideration")]
    public class InRangeConsideration : Consideration
    {
        public float          maxDistance = 10f;
        public float          maxAngle    = 360f;
        public string         targetTag   = "Target";
        public AnimationCurve curve;

        public override float Evaluate(Context context)
        {
            if (!context.sensor.targetTags.Contains(this.targetTag)) context.sensor.targetTags.Add(this.targetTag);

            var targetTransform = context.sensor.GetClosestTarget(this.targetTag);

            if (targetTransform == null) return 0f;

            var agentTransform = context.agent.transform;

            var isInRange = agentTransform.InRangeOf(targetTransform, this.maxDistance, this.maxAngle);

            if (!isInRange) return 0f;

            var directionToTarget = targetTransform.position - agentTransform.position;
            var distanceToTarget  = directionToTarget.With(y: 0).magnitude;

            var normalizedDistance = Mathf.Clamp01(distanceToTarget / this.maxDistance);

            var utility = this.curve.Evaluate(normalizedDistance);

            return Mathf.Clamp01(utility);
        }

        private void Reset()
        {
            this.curve = new AnimationCurve(
                new Keyframe(0f, 1f),
                new Keyframe(1f, 0f)
            );
        }
    }
}