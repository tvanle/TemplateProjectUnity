namespace tvan.uni.foundation.UniCore.Extension.Unity
{
    using UnityEngine;

    public static class TransformExtension
    {
        public static void With(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            var position = transform.position;
            position           = new Vector3(x ?? position.x, y ?? position.y, z ?? position.z);
            transform.position = position;
        }

        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) { return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z); }

        public static bool InRangeOf(this Transform agent, Transform target, float maxDistance, float maxAngle)
        {
            var directionToTarget = (target.position - agent.position).With(y: 0);
            var distanceToTarget  = directionToTarget.magnitude;

            if (distanceToTarget > maxDistance)
                return false;

            var agentForward  = agent.forward.With(y: 0);
            var angleToTarget = Vector3.Angle(agentForward, directionToTarget);

            return angleToTarget <= maxAngle;
        }
    }
}