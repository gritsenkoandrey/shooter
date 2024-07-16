using Infrastructure.LevelService;
using Infrastructure.SceneLoadService;
using UI;
using UI.ScreenService;
using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class GameState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private IScreenService _screenService;
        private ISceneLoadService _sceneLoadService;
        private ILevelService _levelService;

        public GameState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IScreenService screenService, ISceneLoadService sceneLoadService, ILevelService levelService)
        {
            _screenService = screenService;
            _sceneLoadService = sceneLoadService;
            _levelService = levelService;
        }

        void IEnterState.Enter()
        {
            _screenService.CreateScreen(ScreenType.Game);
            _levelService.OnChangeHealth += OnChangeHealth;
            _levelService.OnChangeKills += OnChangeKills;
        }

        void IExitState.Exit()
        {
            _levelService.OnChangeHealth -= OnChangeHealth;
            _levelService.OnChangeKills -= OnChangeKills;
        }

        private void OnChangeKills(int count)
        {
            if (count <= 0)
            {
                _sceneLoadService.Load(SceneName.Result, Win);
            }
        }

        private void OnChangeHealth(int health)
        {
            if (health <= 0)
            {
                _sceneLoadService.Load(SceneName.Result, Lose);
            }
        }

        private void Lose() => _gameStateMachine.Enter<ResultState, bool>(false);
        private void Win() => _gameStateMachine.Enter<ResultState, bool>(true);
    }
}