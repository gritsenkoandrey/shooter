using Game.Gameplay.Jobs;
using Game.Infrastructure.ObjectPoolService;
using Game.Infrastructure.StaticDataService;
using Unity.Jobs;
using UnityEngine.Jobs;
using VContainer;

namespace Game.Infrastructure.GameStateMachine.States
{
    public sealed class PrepareState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;

        private IStaticDataService _staticDataService;
        private IObjectPoolService _objectPoolService;

        public PrepareState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IStaticDataService staticDataService, IObjectPoolService objectPoolService)
        {
            _staticDataService = staticDataService;
            _objectPoolService = objectPoolService;
        }

        void IEnterState.Enter()
        {
            _staticDataService.Load();
            _objectPoolService.Init();
            
            WarmUpJob();
            
            _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Game);
        }

        void IExitState.Exit()
        {
        }
        
        private static void WarmUpJob()
        {
            IJobParallelForExtensions.EarlyJobInit<BulletCollisionJob>();
            IJobParallelForTransformExtensions.EarlyJobInit<BulletMovementJob>();
            IJobParallelForTransformExtensions.EarlyJobInit<EnemyMovementJob>();
        }
    }
}