using Infrastructure.LoadingScreenService;
using UI;
using UI.ScreenService;
using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class ResultState : IEnterLoadState<bool>
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private IScreenService _screenService;
        private ILoadingScreenService _loadingScreenService;

        private BaseScreen _screen;

        public ResultState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IScreenService screenService, ILoadingScreenService loadingScreenService)
        {
            _screenService = screenService;
            _loadingScreenService = loadingScreenService;
        }

        void IEnterLoadState<bool>.Enter(bool isWin)
        {
            _screen = _screenService.CreateScreen(isWin ? ScreenType.Win : ScreenType.Lose);
            _screen.OnClose += Next;
        }

        void IExitState.Exit()
        {
            _screen.OnClose -= Next;
            _screen = null;
        }

        private void Next()
        {
            _loadingScreenService.Show();
            _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Game);
        }
    }
}