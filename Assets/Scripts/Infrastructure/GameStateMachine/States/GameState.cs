using Game.Gameplay.Models;
using Game.Infrastructure.InputService;
using Game.Infrastructure.LoadingScreenService;
using Game.Infrastructure.SceneLoadService;
using Game.Scopes;
using Game.UI;
using Game.UI.ScreenService;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.GameStateMachine.States
{
    public sealed class GameState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private IScreenService _screenService;
        private ISceneLoadService _sceneLoadService;
        private ILoadingScreenService _loadingScreenService;
        private IInputService _inputService;
        private PlayerHealth _playerHealth;
        private EnemyKillCounter _enemyKillCounter;

        public GameState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IScreenService screenService, ISceneLoadService sceneLoadService, 
            ILoadingScreenService loadingScreenService, IInputService inputService)
        {
            _screenService = screenService;
            _sceneLoadService = sceneLoadService;
            _loadingScreenService = loadingScreenService;
            _inputService = inputService;
        }

        void IEnterState.Enter()
        {
            _screenService.CreateScreen(ScreenType.Game);
            _loadingScreenService.Hide();
            _inputService.Enable(true);

            IObjectResolver resolver = LifetimeScope.Find<GameScope>().Container;
            
            _playerHealth = resolver.Resolve<PlayerHealth>();
            _enemyKillCounter = resolver.Resolve<EnemyKillCounter>();
            
            _playerHealth.OnChangeHealth += OnChangeHealth;
            _enemyKillCounter.OnChangeKills += OnChangeKills;
        }

        void IExitState.Exit()
        {
            _inputService.Enable(false);
            _playerHealth.OnChangeHealth -= OnChangeHealth;
            _enemyKillCounter.OnChangeKills -= OnChangeKills;
            _playerHealth = null;
            _enemyKillCounter = null;
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