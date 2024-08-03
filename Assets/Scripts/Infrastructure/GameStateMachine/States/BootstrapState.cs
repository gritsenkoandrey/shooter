using Game.Infrastructure.LoadingScreenService;
using Game.Infrastructure.SceneLoadService;
using VContainer;

namespace Game.Infrastructure.GameStateMachine.States
{
    public sealed class BootstrapState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private ISceneLoadService _sceneLoadService;
        private ILoadingScreenService _loadingScreenService;

        public BootstrapState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(ISceneLoadService sceneLoadService, ILoadingScreenService loadingScreenService)
        {
            _sceneLoadService = sceneLoadService;
            _loadingScreenService = loadingScreenService;
        }

        void IEnterState.Enter()
        {
            _loadingScreenService.Show();
            _sceneLoadService.Load(SceneName.Bootstrap, Next);
        }

        void IExitState.Exit()
        {
        }

        private void Next() => _gameStateMachine.Enter<PrepareState>();
    }
}