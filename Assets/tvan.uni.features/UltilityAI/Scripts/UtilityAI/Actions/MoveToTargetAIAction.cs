namespace UtilityAI
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "UtilityAI/Actions/MoveToTargetAction")]
    public class MoveToTargetAIAction : AIAction
    {
        public override void Initialize(Context context) { context.sensor.targetTags.Add(this.targetTag); }

        public override void Execute(Context context)
        {
            var target = context.sensor.GetClosestTarget(this.targetTag);

            if (target == null) return;

            context.target = target;

            context.agent.SetDestination(target.position);
        }
    }
}