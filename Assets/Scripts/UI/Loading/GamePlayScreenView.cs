namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Tvan.Foundation.UniCore.SignalBus;
    using Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Tvan.Foundation.UniUI.Scripts.BaseScreen.View;

    public class GamePlayScreenView : BaseView
    {
    }

    public class GamePlayModel
    {
    }

    [ScreenInfo(nameof(GamePlayScreenView))]
    public class GamePlayScreenPresenter : BaseScreenPresenter<GamePlayScreenView, GamePlayModel>
    {
        public GamePlayScreenPresenter(SignalBus signalBus)
            : base(signalBus)
        {
        }

        public override async UniTask BindData() { }
    }
}