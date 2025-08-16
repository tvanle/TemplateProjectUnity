namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Tvan.Foundation.UniCore.SignalBus;
    using Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Tvan.Foundation.UniUI.Scripts.BaseScreen.View;

    public class LoseScreenView : BaseView
    {
    }

    public class LoseModel
    {
    }

    [ScreenInfo(nameof(LoseScreenView))]
    public class LoseScreenPresenter : BaseScreenPresenter<LoseScreenView, LoseModel>
    {
        public LoseScreenPresenter(SignalBus signalBus)
            : base(signalBus)
        {
        }

        public override async UniTask BindData() { }
    }
}