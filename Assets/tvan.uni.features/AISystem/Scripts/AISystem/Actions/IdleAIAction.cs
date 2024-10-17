namespace tvan.uni.features.AISystem.Scripts.AISystem.Actions
{
    using tvan.uni.features.AISystem.Scripts.AISystem.Brain;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UtilityAI/Actions/IdleAction")]
    public class IdleAIAction : AIAction
    {
        public override void Execute(Context context) { context.agent.SetDestination(context.agent.transform.position); }
    }
}