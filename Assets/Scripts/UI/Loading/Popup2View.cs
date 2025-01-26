namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.View;
    using tvan.uni.foundation.UniCore.SignalBus;

    public class Popup2View : BaseView
    {
    }

    public class Popup2Model
    {
    }

    [ScreenInfo(nameof(Popup2View))]
    public class Popup2Presenter : BasePopupPresenter<Popup2View, Popup2Model>
    {
        public Popup2Presenter(SignalBus signalBus)
            : base(signalBus)
        {
        }

        public override async UniTask BindData() { }
    }
}