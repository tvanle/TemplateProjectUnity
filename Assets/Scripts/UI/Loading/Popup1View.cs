namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.View;
    using tvan.uni.foundation.UniCore.SignalBus;

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