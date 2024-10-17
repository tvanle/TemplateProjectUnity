namespace UtilityAI
{
    using UnityEngine;

    public class DrawGizmo : MonoBehaviour
    {
        public Color color  = Color.yellow;
        public float radius = 1f;

        [SerializeField] private SphereCollider collider;

        private void Start()
        {
            if (this.collider == null) this.collider = this.GetComponent<SphereCollider>();
            this.radius = this.collider.radius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = this.color;
            Gizmos.DrawWireSphere(this.transform.position, this.radius);
        }
    }
}