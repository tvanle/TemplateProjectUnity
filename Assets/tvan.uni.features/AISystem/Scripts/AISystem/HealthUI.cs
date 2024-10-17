namespace tvan.uni.features.AISystem.Scripts.AISystem
{
    using tvan.uni.features.AISystem.Scripts.AISystem.Ultils;
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private ProceduralProgressBar progressBar;
        [SerializeField] private Button                healthButton;
        [SerializeField] private Button                damageButton;
        [SerializeField] private Health                health;

        private void Start()
        {
            this.healthButton.onClick.AddListener(() => this.Heal(10));
            this.damageButton.onClick.AddListener(() => this.TakeDamage(10));
        }

        private void Update()
        {
            if (this.progressBar) this.progressBar.Value = this.health.Current / this.health.maxHealth;
        }

        private void Heal(float value) { this.health.Heal(value); }

        private void TakeDamage(float damage) { this.health.TakeDamage(damage); }
    }
}