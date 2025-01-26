namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.View;
    using tvan.uni.foundation.UniCore.SignalBus;

    public class WinScreenView : BaseView
    {
    }

    public class WinScreenModel
    {
    }

    [ScreenInfo(nameof(WinScreenView))]
    public class WinScreenPresenter : BaseScreenPresenter<WinScreenView, WinScreenModel>
    {
        public WinScreenPresenter(SignalBus signalBus)
            : base(signalBus)
        {
        }

        public override async UniTask BindData() { }
    }
}