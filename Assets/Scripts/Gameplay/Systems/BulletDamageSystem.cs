using Core.Implementation;
using Gameplay.Components;
using Infrastructure.LevelService;
using UnityEngine;
using Utils;
using VContainer;

namespace Gameplay.Systems
{
    public sealed class BulletDamageSystem : SystemComponent<Bullet>
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

        private void Damage(Bullet bullet)
        {
            for (int i = _levelService.Enemies.Count - 1; i >= 0; i--)
            {
                float distance = (_levelService.Enemies[i].Position - bullet.Position).sqrMagnitude;

                if (distance < _levelService.Enemies[i].CollisionRadius + bullet.CollisionRadius)
                {
                    _levelService.Enemies[i].Health -= bullet.Damage;

                    if (_levelService.Enemies[i].Health <= 0)
                    {
                        Object.Destroy(_levelService.Enemies[i].gameObject);
                        
                        _levelService.Enemies.RemoveAt(i);
                        _levelService.Kills--;
                    }
                    
                    Object.Destroy(bullet.gameObject);
                }
            }
        }
    }
}