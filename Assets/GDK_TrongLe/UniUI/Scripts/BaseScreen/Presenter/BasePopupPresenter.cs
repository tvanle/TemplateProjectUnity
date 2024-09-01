namespace GDK_TrongLe.UniUI.Scripts.BaseScreen.Presenter
{
    using GDK_TrongLe.UniUI.Scripts.BaseScreen.View;

    public abstract class BasePopupPresenter<TView> : BaseScreenPresenter<TView>, IPopupPresenter where TView : IScreenView
    {
    }

    public abstract class BasePopupPresenter<TView, TModel> : BasePopupPresenter<TView> where TView : IScreenView
    {
    }
}