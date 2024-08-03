using Game.UI;
using Game.UI.ScreenService;
using VContainer;

namespace Game.Infrastructure.GameStateMachine.States
{
    public sealed class ResultState : IEnterLoadState<bool>
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private IScreenService _screenService;

        public ResultState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IScreenService screenService)
        {
            _screenService = screenService;
        }

        void IEnterLoadState<bool>.Enter(bool isWin)
        {
            BaseScreen screen = _screenService.CreateScreen(isWin ? ScreenType.Win : ScreenType.Lose);
            
            screen.OnClose += Next;

            void Next()
            {
                screen.OnClose -= Next;
                
                _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Game);
            }
        }

        void IExitState.Exit()
        {
        }
    }
}