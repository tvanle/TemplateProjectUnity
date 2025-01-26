namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.View;
    using tvan.uni.foundation.UniCore.SignalBus;

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