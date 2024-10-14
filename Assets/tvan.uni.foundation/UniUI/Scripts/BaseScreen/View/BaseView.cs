namespace tvan.uni.foundation.UniUI.Scripts.BaseScreen.View
{
    using System;
    using Cysharp.Threading.Tasks;
    using tvan.uni.foundation.UniUI.Scripts.Utilities.UIStuff;
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    public class BaseView : MonoBehaviour, IScreenView
    {
        [SerializeField] private CanvasGroup        viewRoot;
        [SerializeField] private UIScreenTransition screenTransition;
        public event Action                         ViewDidClose;
        public event Action                         ViewDidOpen;
        public event Action                         ViewDidDestroy;

        protected         UIScreenTransition ScreenTransition => this.screenTransition;
        protected virtual CanvasGroup        ViewRoot         { get => this.viewRoot; set => this.viewRoot = value; }
        public            RectTransform      RectTransform    { get;                  private set; }

        private void Awake()
        {
            // This will allow to set the view in the inspector if we want to
            if (!this.ViewRoot) this.ViewRoot = this.GetComponent<CanvasGroup>();

            this.screenTransition = this.ScreenTransition ? this.ScreenTransition : this.GetComponent<UIScreenTransition>();

            if (this.ScreenTransition == null) Debug.LogError($"Can not find UIScreenTransition component in {this.gameObject.name} screen", this);

            this.RectTransform = this.GetComponent<RectTransform>();

            // Set the alpha to zero so the item is created
            // invisible. When the show method is called
            // the view will be made visible using a transition.
            this.UpdateAlpha(0);
            this.IsReadyToUse = true;
        }

        private void Start() { }

        private void OnDestroy()
        {
            this.ViewDidDestroy?.Invoke();
        }

        public bool IsReadyToUse { get; private set; }

        public virtual async UniTask Open()
        {
            this.Show();
            await this.ScreenTransition.PlayIntroAnim();
            Debug.Log($"open screen view {this.name}");
            this.ViewDidOpen?.Invoke();
        }

        public virtual async UniTask Close()
        {
            await this.ScreenTransition.PlayOutroAnim();
            Debug.Log($"Close screen view {this.name}");
            this.Hide();
            this.ViewDidClose?.Invoke();
        }

        public void Hide() { this.UpdateAlpha(0); }

        public void Show() { this.UpdateAlpha(1); }

        public void DestroySelf() { Destroy(this.gameObject); }

        protected void UpdateAlpha(float value)
        {
            this.ViewRoot.alpha          = value;
            this.ViewRoot.blocksRaycasts = value >= 1;
        }
    }
}