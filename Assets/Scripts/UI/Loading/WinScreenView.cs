namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Tvan.Foundation.UniCore.SignalBus;
    using Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Tvan.Foundation.UniUI.Scripts.BaseScreen.View;

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