using GDK_TrongLe.UniCore.Extension.Unity;
using Packages.Tvan.Foundation.UniUI.Scripts.Manager;
using UI.Loading;
using UnityEngine;

public class TestScreen : MonoBehaviour
{
    private IScreenManager screenManager;

    public IScreenManager ScreenManager
    {
        get
        {
            if (this.screenManager != null) return this.screenManager;
            this.screenManager = this.GetCurrentContainer().Resolve<IScreenManager>();

            return this.screenManager;
        }
    }

    public void OpenGamePlay() { this.ScreenManager.OpenScreen<GamePlayScreenPresenter, GamePlayModel>(new GamePlayModel()); }

    public void OpenLose() { this.ScreenManager.OpenScreen<LoseScreenPresenter, LoseModel>(new LoseModel()); }

    public void OpenWin() { this.ScreenManager.OpenScreen<WinScreenPresenter, WinScreenModel>(new WinScreenModel()); }

    public void OpenPopup1() { this.ScreenManager.OpenScreen<Popup1Presenter, Popup1Model>(new Popup1Model()); }

    public void OpenPopup2() { this.ScreenManager.OpenScreen<Popup2Presenter, Popup2Model>(new Popup2Model()); }

    public void OpenPopup3() { this.ScreenManager.OpenScreen<Popup3Presenter, Popup3Model>(new Popup3Model()); }
}