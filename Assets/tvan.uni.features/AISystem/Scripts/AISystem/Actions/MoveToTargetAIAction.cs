namespace tvan.uni.features.AISystem.Scripts.AISystem.Actions
{
    using tvan.uni.features.AISystem.Scripts.AISystem.Brain;
    using tvan.uni.foundation.UniCore.Extension.Unity;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UtilityAI/Actions/MoveToTargetAction")]
    public class MoveToTargetAIAction : AIAction
    {
        public override void Initialize(Context context) { context.sensor.targetTags.Add(this.targetTag); }

        public override void Execute(Context context)
        {
            var target = context.sensor.GetClosestTarget(this.targetTag);

            if (target.IsNull()) return;

            context.target = target;

            context.agent.SetDestination(target.position);
        }
    }
}