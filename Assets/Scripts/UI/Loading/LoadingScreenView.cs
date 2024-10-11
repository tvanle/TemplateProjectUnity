namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using DG.Tweening;
    using GDK_TrongLe.UniCore.Extension.Unity;
    using TMPro;
    using tvan.uni.foundation.UniCore.AssetLibrary.Scripts;
    using tvan.uni.foundation.UniData.Scripts.Csv.CsvController;
    using tvan.uni.foundation.UniData.Scripts.LocalData.Manager;
    using tvan.uni.foundation.UniUI.Scripts.BaseScreen.Presenter;
    using tvan.uni.foundation.UniUI.Scripts.BaseScreen.View;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine.ResourceManagement.ResourceProviders;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class LoadingScreenView : BaseView
    {
        [SerializeField] private Slider          loadingSlider;
        [SerializeField] private TextMeshProUGUI loadingProgressTxt;

        private  Tween  tween;
        private  float  trueProgress;
        internal string LoadingText;

        private void Start() { this.loadingSlider.value = 0f; }

        public void SetProgress(float progress)
        {
            if (!this.loadingSlider) return;
            if (progress <= this.trueProgress) return;
            this.tween.Kill();
            this.tween = DOTween.To(
                () => this.loadingSlider.value,
                value =>
                {
                    this.loadingSlider.value = value;
                    if (this.loadingProgressTxt != null)
                        this.loadingProgressTxt.text = string.Format(this.LoadingText, (int)(value * 100));
                },
                this.trueProgress = progress,
                0.5f
            ).SetUpdate(true);
        }

        public UniTask CompleteLoading()
        {
            this.SetProgress(1f);

            return this.tween.AsyncWaitForCompletion().AsUniTask();
        }
    }

    [ScreenInfo(nameof(LoadingScreenView))]
    public class LoadingScreenPresenter : BaseScreenNormalPresenter<LoadingScreenView>
    {
        protected readonly CsvReaderManager CsvManager;
        protected readonly LocalDataManager LocalDataManager;
        protected readonly IGameAssets      GameAssets;

        public LoadingScreenPresenter(CsvReaderManager csvManager,
                                      LocalDataManager localDataManager,
                                      IGameAssets gameAssets)
        {
            this.CsvManager       = csvManager;
            this.LocalDataManager = localDataManager;
            this.GameAssets       = gameAssets;
        }

        private float      loadingProgress;
        private int        loadingSteps;
        private GameObject objectPoolContainer;

        protected virtual string NextSceneName => "1.MainScene";

        private float LoadingProgress
        {
            get => this.loadingProgress;
            set
            {
                this.loadingProgress = value;
                this.View.SetProgress(value / this.loadingSteps);
            }
        }

        protected override async void OnViewReady()
        {
            base.OnViewReady();

            this.objectPoolContainer = new GameObject(nameof(this.objectPoolContainer));
            Object.DontDestroyOnLoad(this.objectPoolContainer);

            this.LoadingProgress = 0f;
            this.loadingSteps    = 1;

            await UniTask.WhenAll(
                this.Preload(),
                UniTask.WhenAll(
                    this.LoadBlueprint().ContinueWith(this.OnBlueprintLoaded),
                    this.LoadUserData().ContinueWith(this.OnUserDataLoaded)
                ).ContinueWith(this.OnBlueprintAndUserDataLoaded)
            ).ContinueWith(this.OnLoadingCompleted).ContinueWith(this.LoadNextScene);
        }

        public override async UniTask BindData() { await UniTask.CompletedTask; }

        protected virtual async UniTask LoadNextScene()
        {
            var nextScene = await this.TrackProgress(this.LoadSceneAsync());
            await this.View.CompleteLoading();
            await nextScene.ActivateAsync();
        }

        protected virtual AsyncOperationHandle<SceneInstance> LoadSceneAsync() { return this.GameAssets.LoadSceneAsync(this.NextSceneName, LoadSceneMode.Single, false); }

        private UniTask LoadUserData() { return this.TrackProgress(this.LocalDataManager.LoadUserData()); }

        private UniTask LoadBlueprint() { return this.CsvManager.LoadCsvData(); }

        protected virtual UniTask OnBlueprintLoaded() { return UniTask.CompletedTask; }

        protected virtual UniTask OnUserDataLoaded() { return UniTask.CompletedTask; }

        protected virtual UniTask OnBlueprintAndUserDataLoaded() { return UniTask.CompletedTask; }

        protected virtual UniTask OnLoadingCompleted() { return UniTask.CompletedTask; }

        protected virtual UniTask Preload() { return UniTask.CompletedTask; }

        protected virtual UniTask PreloadAssets<T>(params object[] keys)
        {
            return UniTask.WhenAll(this.GameAssets.PreloadAsync<T>(this.NextSceneName, keys)
                                       .Select(this.TrackProgress));
        }

        protected virtual UniTask TrackProgress(UniTask task)
        {
            ++this.loadingSteps;

            return task.ContinueWith(() => ++this.LoadingProgress);
        }

        protected virtual UniTask<T> TrackProgress<T>(AsyncOperationHandle<T> aoh)
        {
            ++this.loadingSteps;
            var localLoadingProgress = 0f;

            void UpdateProgress(float progress)
            {
                this.LoadingProgress += progress - localLoadingProgress;
                localLoadingProgress =  progress;
            }

            return aoh.ToUniTask(Progress.CreateOnlyValueChanged<float>(UpdateProgress))
                      .ContinueWith(result =>
                      {
                          UpdateProgress(1f);

                          return result;
                      });
        }
    }
}