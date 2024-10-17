namespace tvan.uni.features.AISystem.Scripts.AISystem.Ultils
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ProceduralProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progressBarFill;
        [SerializeField] private float speed = 0.1f;

        private float currentValue;

        public float Value
        {
            get => this.currentValue;
            set
            {
                this.currentValue = Mathf.Clamp01(value);
                this.UpdateProgressBar();
            }
        }

        private void UpdateProgressBar()
        {
            if (this.progressBarFill != null)
                this.progressBarFill.fillAmount =
                    Mathf.Lerp(this.progressBarFill.fillAmount, this.currentValue, this.speed);
        }
    }
}