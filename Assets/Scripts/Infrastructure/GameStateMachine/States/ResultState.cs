using Game.Infrastructure.LoadingScreenService;
using Game.UI;
using Game.UI.ScreenService;
using VContainer;

namespace Game.Infrastructure.GameStateMachine.States
{
    public sealed class ResultState : IEnterLoadState<bool>
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private IScreenService _screenService;
        private ILoadingScreenService _loadingScreenService;

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
            BaseScreen screen = _screenService.CreateScreen(isWin ? ScreenType.Win : ScreenType.Lose);
            
            screen.OnClose += Next;

            void Next()
            {
                screen.OnClose -= Next;
                
                _loadingScreenService.Show();
                _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Game);
            }
        }

        void IExitState.Exit()
        {
        }
    }
}