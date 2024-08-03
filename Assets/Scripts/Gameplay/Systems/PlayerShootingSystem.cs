using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Game.Core.Implementation;
using Game.Gameplay.Entities;
using Game.Infrastructure.Factories.GameFactory;
using Game.Infrastructure.LevelService;
using Game.Utils;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Systems
{
    public sealed class PlayerShootingSystem : SystemComponent<Player>
    {
        private IGameFactory _gameFactory;
        private ILevelService _levelService;

        [Inject]
        private void Construct(IGameFactory gameFactory, ILevelService levelService)
        {
            _gameFactory = gameFactory;
            _levelService = levelService;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Execute);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Execute(Player component)
        {
            if (CanShoot(component, _levelService.Enemies))
            {
                int index = GetNearestTarget(component, _levelService.Enemies);

                CreateBullet(component, _levelService.Enemies[index]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CanShoot(Player component, IReadOnlyList<Enemy> enemies)
        {
            if (component.Weapon.CurrentFireInterval > 0f)
            {
                component.Weapon.CurrentFireInterval -= Time.deltaTime;
                
                return false;
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                float sqrMagnitude = (component.Position - enemies[i].Position).sqrMagnitude;

                if (component.Weapon.FireRadius > sqrMagnitude)
                {
                    component.Weapon.CurrentFireInterval = component.Weapon.FireInterval;
                    
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetNearestTarget(Player component, IReadOnlyList<Enemy> enemies)
        {
            int index = 0;
            
            float minDistance = float.MaxValue;

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                float sqrMagnitude = (component.Position - enemies[i].Position).sqrMagnitude;

                if (minDistance > sqrMagnitude)
                {
                    minDistance = sqrMagnitude;
                    
                    index = i;
                }
            }

            return index;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreateBullet(Player component, Enemy enemy)
        {
            Bullet bullet = _gameFactory.CreateBullet(component.Position, Quaternion.identity);
            bullet.SetDirection((enemy.Position - component.Position).normalized);
            bullet.SetSpeed(component.Weapon.BulletSpeed);
            bullet.SetDamage(component.Weapon.BulletDamage);
        }
    }
}