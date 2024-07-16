namespace UI.Screens
{
    public sealed class LoseScreen : BaseScreen
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            
            _button.onClick.AddListener(Close);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _button.onClick.RemoveListener(Close);
        }
    }
}