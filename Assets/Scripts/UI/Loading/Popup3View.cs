namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Tvan.Foundation.UniCore.SignalBus;
    using Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Tvan.Foundation.UniUI.Scripts.BaseScreen.View;

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