namespace tvan.uni.foundation.UniUI.Scripts.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Cysharp.Threading.Tasks;
    using GDK_TrongLe.UniCore.Extension.Unity;
    using tvan.uni.foundation.UniCore.AssetLibrary.Scripts;
    using tvan.uni.foundation.UniCore.Extension;
    using tvan.uni.foundation.UniUI.Scripts.BaseScreen.Presenter;
    using tvan.uni.foundation.UniUI.Scripts.BaseScreen.View;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public interface IScreenManager
    {
        public UniTask<T> GetScreen<T>() where T : IScreenPresenter;

        public UniTask<T> OpenScreen<T>() where T : IScreenPresenter;

        public UniTask<TPresenter> OpenScreen<TPresenter, TModel>(TModel model) where TPresenter : IScreenPresenter<TModel>;

        public Transform CurrentRootScreen { get; set; }

        public Transform CurrentHiddenRoot  { get; set; }
        public Transform CurrentOverlayRoot { get; set; }

        public RootUICanvas RootUICanvas { get; set; }
    }

    public class ScreenManager : IScreenManager, IDisposable
    {
        #region Inject

        private readonly IGameAssets gameAssets;

        public ScreenManager(IGameAssets gameAssets)
        {
            this.gameAssets = gameAssets;

            this.activeScreens               = new List<IScreenPresenter>();
            this.typeToLoadedScreenPresenter = new Dictionary<Type, IScreenPresenter>();
            this.typeToPendingScreen         = new Dictionary<Type, Task<IScreenPresenter>>();
        }

        #endregion

        #region Properties

        [SerializeField] private List<IScreenPresenter> activeScreens;
        private                  IScreenPresenter       previousActiveScreen;

        private Dictionary<Type, IScreenPresenter>       typeToLoadedScreenPresenter;
        private Dictionary<Type, Task<IScreenPresenter>> typeToPendingScreen;

        private RootUICanvas rootUICanvas;
        private bool         enableBackToClose = false;

        #endregion

        public void Dispose() { }

        public void EnableBackToClose(bool enable) { this.enableBackToClose = enable; }

        #region Implement IScreenManager

        public Transform    CurrentRootScreen  { get; set; }
        public Transform    CurrentHiddenRoot  { get; set; }
        public Transform    CurrentOverlayRoot { get; set; }
        public RootUICanvas RootUICanvas       { get; set; }

        public async UniTask<T> OpenScreen<T>() where T : IScreenPresenter
        {
            var nextScreen = await this.GetScreen<T>();

            if (nextScreen != null)
            {
                await nextScreen.OpenViewAsync();

                return nextScreen;
            }
            else
            {
                Debug.LogError($"The {typeof(T).Name} screen does not exist");

                // Need to implement lazy initialization by Load from resource
                return default;
            }
        }

        public async UniTask<TPresenter> OpenScreen<TPresenter, TModel>(TModel model) where TPresenter : IScreenPresenter<TModel>
        {
            var nextScreen = (await this.GetScreen<TPresenter>());

            if (nextScreen != null)
            {
                nextScreen.SetViewParent(this.CheckPopupIsOverlay(nextScreen) ? this.CurrentOverlayRoot : this.CurrentRootScreen);
                await nextScreen.OpenView(model);

                return nextScreen;
            }
            else
            {
                Debug.LogError($"The {typeof(TPresenter).Name} screen does not exist");

                // Need to implement lazy initialization by Load from resource
                return default;
            }
        }

        public async UniTask<T> GetScreen<T>() where T : IScreenPresenter
        {
            var screenType = typeof(T);

            if (this.typeToLoadedScreenPresenter.TryGetValue(screenType, out var screenPresenter)) return (T)screenPresenter;

            if (!this.typeToPendingScreen.TryGetValue(screenType, out var loadingTask))
            {
                loadingTask = InstantiateScreen();
                this.typeToPendingScreen.Add(screenType, loadingTask);
            }

            var result = await loadingTask;
            this.typeToPendingScreen.Remove(screenType);

            return (T)result;

            async Task<IScreenPresenter> InstantiateScreen()
            {
                screenPresenter = this.GetCurrentContainer().Instantiate<T>();
                var screenInfo = screenPresenter.GetCustomAttribute<ScreenInfoAttribute>();

                var viewObject = Object.Instantiate(await this.gameAssets.LoadAssetAsync<GameObject>(screenInfo.AddressableScreenPath),
                    this.CheckPopupIsOverlay(screenPresenter) ? this.CurrentOverlayRoot : this.CurrentRootScreen).GetComponent<IScreenView>();

                screenPresenter.SetView(viewObject);
                this.typeToLoadedScreenPresenter.Add(screenType, screenPresenter);

                return (T)screenPresenter;
            }
        }

        #endregion

        #region Check Overlay Popup

        private bool CheckScreenIsPopup(IScreenPresenter screenPresenter) { return screenPresenter.GetType().IsSubclassOfRawGeneric(typeof(BasePopupPresenter<>)); }

        private bool CheckPopupIsOverlay(IScreenPresenter screenPresenter) { return this.CheckScreenIsPopup(screenPresenter) && screenPresenter.GetCustomAttribute<PopupInfoAttribute>().IsOverlay; }

        #endregion
    }
}