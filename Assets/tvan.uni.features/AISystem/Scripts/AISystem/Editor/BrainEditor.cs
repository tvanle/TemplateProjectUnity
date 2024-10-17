namespace tvan.uni.features.AISystem.Scripts.AISystem.Editor
{
    using tvan.uni.features.AISystem.Scripts.AISystem.Actions;
    using tvan.uni.features.AISystem.Scripts.AISystem.Brain;
    using tvan.uni.features.AISystem.Scripts.AISystem.Considerations;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Brain))]
    public class BrainEditor : Editor
    {
        private void OnEnable() { this.RequiresConstantRepaint(); }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI(); // Draw the default inspector

            var brain = (Brain)this.target;

            if (Application.isPlaying)
            {
                var chosenAction = this.GetChosenAction(brain);

                if (chosenAction != null) EditorGUILayout.LabelField($"Current Chosen Action: {chosenAction.name}", EditorStyles.boldLabel);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Actions/Considerations", EditorStyles.boldLabel);

                foreach (var action in brain.actions)
                {
                    var utility = action.CalculateUtility(brain.context);
                    EditorGUILayout.LabelField($"Action: {action.name}, Utility: {utility:F2}");

                    // Draw the single consideration for the action
                    this.DrawConsideration(action.consideration, brain.context, 1);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Enter Play mode to view utility values.", MessageType.Info);
            }
        }

        private void DrawConsideration(Consideration consideration, Context context, int indentLevel)
        {
            EditorGUI.indentLevel = indentLevel;

            if (consideration is CompositeConsideration compositeConsideration)
            {
                EditorGUILayout.LabelField(
                    $"Composite Consideration: {compositeConsideration.name}, Operation: {compositeConsideration.operation}"
                );

                foreach (var subConsideration in compositeConsideration.considerations) this.DrawConsideration(subConsideration, context, indentLevel + 1);
            }
            else
            {
                var value = consideration.Evaluate(context);
                EditorGUILayout.LabelField($"Consideration: {consideration.name}, Value: {value:F2}");
            }

            EditorGUI.indentLevel = indentLevel - 1; // Reset indentation after drawing
        }

        private AIAction GetChosenAction(Brain brain)
        {
            var      highestUtility = float.MinValue;
            AIAction chosenAction   = null;

            foreach (var action in brain.actions)
            {
                var utility = action.CalculateUtility(brain.context);
                if (utility > highestUtility)
                {
                    highestUtility = utility;
                    chosenAction   = action;
                }
            }

            return chosenAction;
        }
    }
}