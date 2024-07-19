namespace GDK_TrongLe.UniUI.Scripts.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cysharp.Threading.Tasks;
    using GDK_TrongLe.UniCore.AssetLibrary.Scripts;
    using GDK_TrongLe.UniCore.Extension;
    using GDK_TrongLe.UniCore.SignalBus;
    using GDK_TrongLe.UniUI.Scripts.BaseScreen.Presenter;
    using GDK_TrongLe.UniUI.Scripts.BaseScreen.View;
    using UnityEngine;
    using Zenject;

    public interface IScreenManager
    {
        /// <summary>
        /// Get instance of a screen
        /// </summary>
        /// <typeparam name="T">Type of screen presenter</typeparam>
        public UniTask<T> GetScreen<T>() where T : IScreenPresenter;

        /// <summary>
        /// Open a screen by type
        /// </summary>
        /// <typeparam name="T">Type of screen presenter</typeparam>
        public UniTask<T> OpenScreen<T>() where T : IScreenPresenter;

        public UniTask<TPresenter> OpenScreen<TPresenter, TModel>(TModel model) where TPresenter : IScreenPresenter<TModel>;

        /// <summary>
        /// Close a screen on top
        /// </summary>
        public UniTask CloseCurrentScreen();

        /// <summary>
        /// Close all screen on current scene
        /// </summary>
        public void CloseAllScreen();

        /// <summary>
        /// Close all screen on current scene async
        /// </summary>
        public UniTask CloseAllScreenAsync();

        /// <summary>
        /// Get root transform of all screen, used as the parent transform of each screen
        /// </summary>
        public Transform CurrentRootScreen { get; set; }

        public Transform CurrentHiddenRoot { get; set; }

        /// <summary>
        /// Get overlay transform
        /// </summary>
        public Transform CurrentOverlayRoot { get; set; }

        /// <summary>
        /// Get root canvas of all screen, use to disable UI for creative purpose
        /// </summary>
        public RootUICanvas RootUICanvas { get; set; }
        
    }

    public class ScreenManager : MonoBehaviour, IScreenManager, IDisposable
    {
        #region Properties

        /// <summary>
        /// List of active screens
        /// </summary>
        [SerializeField] private List<IScreenPresenter> activeScreens;

        /// <summary>
        /// Current screen shown on top.
        /// </summary>
        private IScreenPresenter previousActiveScreen;

        private Dictionary<Type, IScreenPresenter>       typeToLoadedScreenPresenter;
        private Dictionary<Type, Task<IScreenPresenter>> typeToPendingScreen;

        private SignalBus    signalBus;
        private RootUICanvas rootUICanvas;
        private IGameAssets  gameAssets;
        private bool         enableBackToClose = false;

        #endregion

        [Inject]
        public void Init(SignalBus signalBusParam, IGameAssets gameAssetsParam)
        {
            this.signalBus  = signalBusParam;
            this.gameAssets = gameAssetsParam;

            this.activeScreens               = new List<IScreenPresenter>();
            this.typeToLoadedScreenPresenter = new Dictionary<Type, IScreenPresenter>();
            this.typeToPendingScreen         = new Dictionary<Type, Task<IScreenPresenter>>();
        }

        public void Dispose()
        {
        }

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

                var viewObject = Instantiate(await this.gameAssets.LoadAssetAsync<GameObject>(screenInfo.AddressableScreenPath),
                    this.CheckPopupIsOverlay(screenPresenter) ? this.CurrentOverlayRoot : this.CurrentRootScreen).GetComponent<IScreenView>();

                screenPresenter.SetView(viewObject);
                this.typeToLoadedScreenPresenter.Add(screenType, screenPresenter);

                return (T)screenPresenter;
            }
        }

        public async UniTask CloseCurrentScreen()
        {
            if (this.activeScreens.Count > 0)
                await this.activeScreens.Last().CloseViewAsync();
        }

        public void CloseAllScreen()
        {
            var cacheActiveScreens = this.activeScreens.ToList();
            this.activeScreens.Clear();

            foreach (var screen in cacheActiveScreens)
            {
                screen.CloseViewAsync();
            }
        }

        public async UniTask CloseAllScreenAsync()
        {
            var tasks              = new List<UniTask>();
            var cacheActiveScreens = this.activeScreens.ToList();
            this.activeScreens.Clear();

            foreach (var screen in cacheActiveScreens)
            {
                tasks.Add(screen.CloseViewAsync());
            }

            this.previousActiveScreen      = null;

            await UniTask.WhenAll(tasks);
        }
        

        #endregion
        
        #region Check Overlay Popup

        private bool CheckScreenIsPopup(IScreenPresenter screenPresenter) { return screenPresenter.GetType().IsSubclassOfRawGeneric(typeof(BasePopupPresenter<>)); }

        private bool CheckPopupIsOverlay(IScreenPresenter screenPresenter) { return this.CheckScreenIsPopup(screenPresenter) && screenPresenter.GetCustomAttribute<PopupInfoAttribute>().IsOverlay; }

        #endregion
        }
    
}