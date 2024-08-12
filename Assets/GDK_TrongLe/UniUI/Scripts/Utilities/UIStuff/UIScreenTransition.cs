namespace GDK_TrongLe.UniUI.Scripts.Utilities.UIStuff
{
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.Playables;

    [RequireComponent(typeof(CanvasGroup))]
    public class UIScreenTransition : MonoBehaviour
    {
        [SerializeField] private PlayableDirector introAnimation;
        [SerializeField] private PlayableDirector outroAnimation;

        [Tooltip("if lockInput = true, disable event system while anim is running and otherwise.")] [SerializeField]
        private bool lockInput = true;

        [SerializeField] private DirectorUpdateMode directorUpdateMode = DirectorUpdateMode.UnscaledGameTime;

        private EventSystem             eventSystem;
        private UniTaskCompletionSource animationTask;

        public PlayableDirector IntroAnimation => this.introAnimation;
        public PlayableDirector OutroAnimation => this.outroAnimation;

        private void Awake()
        {
            this.eventSystem                   = EventSystem.current;
            this.introAnimation.timeUpdateMode = this.directorUpdateMode;
            this.outroAnimation.timeUpdateMode = this.directorUpdateMode;
            if (!this.introAnimation.playableAsset)
            {
                Debug.LogWarning($"Intro Animation for {this.gameObject.name} is not available", this);
            }
            else
            {
                this.introAnimation.playOnAwake =  false;
                this.introAnimation.stopped     += this.OnAnimComplete;
            }

            if (!this.outroAnimation.playableAsset)
            {
                Debug.LogWarning($"Outro animation for {this.gameObject.name} is not available", this);
            }
            else
            {
                this.outroAnimation.playOnAwake =  false;
                this.outroAnimation.stopped     += this.OnAnimComplete;
            }
        }

        public UniTask PlayIntroAnim() { return this.PlayAnim(this.introAnimation); }

        public UniTask PlayOutroAnim() { return this.PlayAnim(this.outroAnimation); }

        private UniTask PlayAnim(PlayableDirector anim)
        {
            if (!anim.playableAsset || this.animationTask?.Task.Status == UniTaskStatus.Pending) return UniTask.CompletedTask;

            this.animationTask = new UniTaskCompletionSource();
            this.SetLockInput(true);

            anim.Play();

            return this.animationTask.Task;
        }

        private void OnAnimComplete(PlayableDirector obj)
        {
            this.animationTask.TrySetResult();
            this.SetLockInput(false);
        }

        private void SetLockInput(bool value)
        {
            if (this.lockInput && this.eventSystem != null) this.eventSystem.enabled = !value;
        }
    }
}