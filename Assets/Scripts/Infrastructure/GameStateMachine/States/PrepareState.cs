using Game.Infrastructure.JobService;
using Game.Infrastructure.ObjectPoolService;
using Game.Infrastructure.StaticDataService;
using VContainer;

namespace Game.Infrastructure.GameStateMachine.States
{
    public sealed class PrepareState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;

        private IStaticDataService _staticDataService;
        private IObjectPoolService _objectPoolService;
        private IJobService _jobService;

        public PrepareState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IStaticDataService staticDataService, IObjectPoolService objectPoolService, IJobService jobService)
        {
            _staticDataService = staticDataService;
            _objectPoolService = objectPoolService;
            _jobService = jobService;
        }

        void IEnterState.Enter()
        {
            _staticDataService.Load();
            _objectPoolService.Init();
            _jobService.Init();
            
            _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Game);
        }

        void IExitState.Exit()
        {
        }
    }
}