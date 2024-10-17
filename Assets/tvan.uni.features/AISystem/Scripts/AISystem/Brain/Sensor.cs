namespace tvan.uni.features.AISystem.Scripts.AISystem.Brain
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(SphereCollider))]
    public class Sensor : MonoBehaviour
    {
        public float        detectionRadius = 10f;
        public List<string> targetTags      = new();

        private readonly List<Transform> detectedObjects = new(10);
        private          SphereCollider  sphereCollider;

        private void Start()
        {
            this.sphereCollider           = this.GetComponent<SphereCollider>();
            this.sphereCollider.isTrigger = true;
            this.sphereCollider.radius    = this.detectionRadius;

            var colliders = Physics.OverlapSphere(this.transform.position, this.detectionRadius);
            foreach (var c in colliders) this.ProcessTrigger(c, transform => this.detectedObjects.Add(transform));
        }

        private void OnTriggerEnter(Collider other) { this.ProcessTrigger(other, transform => this.detectedObjects.Add(transform)); }

        private void OnTriggerExit(Collider other) { this.ProcessTrigger(other, transform => this.detectedObjects.Remove(transform)); }

        private void ProcessTrigger(Collider other, Action<Transform> action)
        {
            if (other.CompareTag("Untagged")) return;

            foreach (var t in this.targetTags)
                if (other.CompareTag(t))
                    action(other.transform);
        }

        public Transform GetClosestTarget(string tag)
        {
            if (this.detectedObjects.Count == 0) return null;

            Transform closestTarget      = null;
            var       closestDistanceSqr = Mathf.Infinity;
            var       currentPosition    = this.transform.position;

            foreach (var potentialTarget in this.detectedObjects)
                if (potentialTarget.CompareTag(tag))
                {
                    var directionToTarget = potentialTarget.position - currentPosition;
                    var dSqrToTarget      = directionToTarget.sqrMagnitude;
                    if (dSqrToTarget < closestDistanceSqr)
                    {
                        closestDistanceSqr = dSqrToTarget;
                        closestTarget      = potentialTarget;
                    }
                }

            return closestTarget;
        }
    }
}