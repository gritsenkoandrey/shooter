using Infrastructure.StaticDataService;
using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class PrepareState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;

        private IStaticDataService _staticDataService;

        public PrepareState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        void IEnterState.Enter()
        {
            _staticDataService.Load();
            _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Game);
        }

        void IExitState.Exit()
        {
        }
    }
}