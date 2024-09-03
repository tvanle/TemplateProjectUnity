namespace tvan.uni.foundation.UniUI.Scripts.BaseScreen.Presenter
{
    using tvan.uni.foundation.UniUI.Scripts.BaseScreen.View;

    public abstract class BaseScreenNormalPresenter<TView> : BaseScreenPresenter<TView>, IScreenNormalPresenter where TView : IScreenView
    {
    }

    public abstract class BAseScreenNormalPresenter<TView, TModel> : BaseScreenNormalPresenter<TView> where TView : IScreenView
    {
    }
}