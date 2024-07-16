namespace UI.ScreenService
{
    public interface IScreenService
    {
        BaseScreen CreateScreen(ScreenType screenType);
        void CleanUp();
    }
}