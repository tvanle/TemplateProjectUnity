namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Tvan.Foundation.UniCore.SignalBus;
    using Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Tvan.Foundation.UniUI.Scripts.BaseScreen.View;

    public class Popup1View : BaseView
    {
    }

    public class Popup1Model
    {
    }

    [ScreenInfo(nameof(Popup1View))]
    public class Popup1Presenter : BasePopupPresenter<Popup1View, Popup1Model>
    {
        public Popup1Presenter(SignalBus signalBus)
            : base(signalBus)
        {
        }

        public override async UniTask BindData() { }
    }
}