namespace UtilityAI
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UtilityAI/Considerations/CompositeConsideration")]
    public class CompositeConsideration : Consideration
    {
        public enum OperationType
        {
            Average,
            Multiply,
            Add,
            Subtract,
            Divide,
            Max,
            Min
        }

        public bool allMustBeNonZero = true;

        public OperationType       operation = OperationType.Max;
        public List<Consideration> considerations;

        public override float Evaluate(Context context)
        {
            if (this.considerations == null || this.considerations.Count == 0) return 0f;

            var result = this.considerations[0].Evaluate(context);

            if (result == 0f && this.allMustBeNonZero) return 0f;

            // Suggestion: Only 2 Considerations per Composite
            for (var i = 1; i < this.considerations.Count; i++)
            {
                var value = this.considerations[i].Evaluate(context);

                if (value == 0f && this.allMustBeNonZero) return 0f;

                switch (this.operation)
                {
                    case OperationType.Average:
                        result = (result + value) / 2;

                        break;
                    case OperationType.Multiply:
                        result *= value;

                        break;
                    case OperationType.Add:
                        result += value;

                        break;
                    case OperationType.Subtract:
                        result -= value;

                        break;
                    case OperationType.Divide:
                        result = value != 0 ? result / value : result; // Prevent division by zero

                        break;
                    case OperationType.Max:
                        result = Mathf.Max(result, value);

                        break;
                    case OperationType.Min:
                        result = Mathf.Min(result, value);

                        break;
                }
            }

            return Mathf.Clamp01(result);
        }
    }
}