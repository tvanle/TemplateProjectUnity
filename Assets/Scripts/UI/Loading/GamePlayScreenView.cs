namespace UI.Loading
{
    using Cysharp.Threading.Tasks;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.Presenter;
    using Packages.Tvan.Foundation.UniUI.Scripts.BaseScreen.View;
    using tvan.uni.foundation.UniCore.SignalBus;

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