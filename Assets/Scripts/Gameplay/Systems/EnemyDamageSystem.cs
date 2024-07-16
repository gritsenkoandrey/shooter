using Core.Implementation;
using Gameplay.Components;
using Infrastructure.LevelService;
using UnityEngine;
using Utils;
using VContainer;

namespace Gameplay.Systems
{
    public sealed class EnemyDamageSystem : SystemComponent<Enemy>
    {
        private ILevelService _levelService;

        [Inject]
        private void Construct(ILevelService levelService)
        {
            _levelService = levelService;
        }
        
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Damage);
        }

        private void Damage(Enemy enemy)
        {
            if (enemy.Position.y < _levelService.Finish + enemy.CollisionRadius)
            {
                _levelService.Health -= 1;
                _levelService.Enemies.Remove(enemy);
                
                Object.Destroy(enemy.gameObject);
            }
        }
    }
}