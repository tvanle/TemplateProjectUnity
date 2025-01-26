namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.View;
    using tvan.uni.foundation.UniCore.SignalBus;

    public class Popup3View : BaseView
    {
    }

    public class Popup3Model
    {
    }

    [ScreenInfo(nameof(Popup3View))]
    public class Popup3Presenter : BasePopupPresenter<Popup3View, Popup3Model>
    {
        public Popup3Presenter(SignalBus signalBus)
            : base(signalBus)
        {
        }

        public override async UniTask BindData() { }
    }
}