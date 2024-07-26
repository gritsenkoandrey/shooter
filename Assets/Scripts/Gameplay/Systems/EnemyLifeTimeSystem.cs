using System.Runtime.CompilerServices;
using Game.Core.Implementation;
using Game.Gameplay.Entities;
using Game.Gameplay.Models;
using Game.Infrastructure.LevelService;
using Game.Infrastructure.ObjectPoolService;
using Game.Utils;
using VContainer;

namespace Game.Gameplay.Systems
{
    public sealed class EnemyLifeTimeSystem : SystemComponent<Enemy>
    {
        private ILevelService _levelService;
        private IObjectPoolService _objectPoolService;
        private PlayerHealth _playerHealth;
        private LevelBounds _levelBounds;
        private EnemyKillCounter _enemyKillCounter;

        [Inject]
        private void Construct(ILevelService levelService, IObjectPoolService objectPoolService, 
            PlayerHealth playerHealth, LevelBounds levelBounds, EnemyKillCounter enemyKillCounter)
        {
            _levelService = levelService;
            _objectPoolService = objectPoolService;
            _playerHealth = playerHealth;
            _levelBounds = levelBounds;
            _enemyKillCounter = enemyKillCounter;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Execute);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Execute(Enemy component)
        {
            if (component.Health.CurrentHealth <= 0)
            {
                _enemyKillCounter.Kills--;
                DestroyEnemy(component);
                
                return;
            }
            
            if (component.Position.y < _levelBounds.BoundaryLine + component.Radius)
            {
                _playerHealth.Health -= 1;
                DestroyEnemy(component);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DestroyEnemy(Enemy component)
        {
            _levelService.RemoveEnemy(component);
            _objectPoolService.ReleaseObject(component.gameObject);
        }
    }
}