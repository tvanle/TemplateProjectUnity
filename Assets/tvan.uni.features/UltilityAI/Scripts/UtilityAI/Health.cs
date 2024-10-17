namespace UtilityAI
{
    using UnityEngine;

    public class Health : MonoBehaviour
    {
        public float maxHealth = 100;
        public float Current;
        public float normalizedHealth => this.Current / this.maxHealth;

        private void Start() { this.Current = this.maxHealth; }

        public void Heal(float value) { this.Current += value; }

        public void TakeDamage(float damage)
        {
            this.Current -= damage;
            if (this.Current <= 0) this.Die();
        }

        private void Die()
        {
            // Destroy(gameObject);
        }
    }
}