namespace tvan.uni.foundation.UniUI.Scripts.Manager
{
    using System;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Zenject;

    /// <summary>
    ///     Every Mono Scene Installer will be inherited this class
    /// </summary>
    public class BaseSceneInstaller : MonoInstaller
    {
        /// <summary>
        ///     Instance of Root UI Canvas on Scene
        /// </summary>
        [SerializeField] protected RootUICanvas rootUICanvas;

        protected IScreenManager screenManager;

        [Inject]
        public void Construct(IScreenManager screenManager) { this.screenManager = screenManager; }

        public override void InstallBindings()
        {
            if (this.rootUICanvas == null) return;
            this.screenManager.RootUICanvas       = this.rootUICanvas;
            this.screenManager.CurrentRootScreen  = this.rootUICanvas.RootUIScreenTransform;
            this.screenManager.CurrentHiddenRoot  = this.rootUICanvas.RootUIClosedTransform;
            this.screenManager.CurrentOverlayRoot = this.rootUICanvas.RootUIOverlayTransform;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            var activeScene  = SceneManager.GetActiveScene();
            var sceneContext = this.GetComponent<SceneContext>();
            if (!sceneContext.AutoInjectInHierarchy)
            {
                sceneContext.AutoInjectInHierarchy = true;
                Debug.LogException(new Exception($"{activeScene.name}: SceneContext AutoInjectInHierarchy should be true, commit this scene please!!!"));
                EditorApplication.delayCall += MarkSceneDirtyOnce;

                void MarkSceneDirtyOnce()
                {
                    // Remove the delegate after executing to prevent multiple calls
                    EditorApplication.delayCall -= MarkSceneDirtyOnce;

                    // Check if the current scene is already marked as dirty to avoid unnecessary operations
                    if (!activeScene.isDirty)
                    {
                        EditorSceneManager.MarkSceneDirty(
                            activeScene
                        );
                        EditorSceneManager.SaveScene(activeScene, activeScene.path);
                    }
                }

                EditorApplication.ExitPlaymode();
            }
        }
#endif
    }
}