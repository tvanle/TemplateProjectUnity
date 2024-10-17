namespace UtilityAI
{
    using UnityEngine;
    using UnityEngine.AI;

    public class AnimationController : MonoBehaviour
    {
        private          Animator     animator;
        private          NavMeshAgent agent;
        private readonly int          speedHash = Animator.StringToHash("Speed");
        private          float        currentSpeed;
        private          float        speedVelocity;
        public           float        smoothTime = 0.3f;

        private void Start()
        {
            this.animator = this.GetComponentInChildren<Animator>();
            this.agent    = this.GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var targetSpeed = this.agent.velocity.magnitude;

            this.currentSpeed = Mathf.SmoothDamp(this.currentSpeed, targetSpeed, ref this.speedVelocity, this.smoothTime);
            this.animator.SetFloat(this.speedHash, this.currentSpeed);
        }
    }
}