using Core.Implementation;
using Gameplay.Components;
using Infrastructure.Factories.GameFactory;
using Infrastructure.LevelService;
using Infrastructure.StaticDataService;
using UnityEngine;
using Utils;
using VContainer;

namespace Gameplay.Systems
{
    public sealed class PlayerDamageSystem : SystemComponent<Player>
    {
        private IGameFactory _gameFactory;
        private IStaticDataService _staticDataService;
        private ILevelService _levelService;

        [Inject]
        private void Construct(IGameFactory gameFactory, IStaticDataService staticDataService, ILevelService levelService)
        {
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
            _levelService = levelService;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Damage);
        }

        private void Damage(Player player)
        {
            if (CanDamage(player))
            {
                int index = FindNearestTarget(player);

                Bullet bullet = _gameFactory.CreateBullet(player.Position, Quaternion.identity, null);

                bullet.Direction = (_levelService.Enemies[index].Position - player.Position).normalized;
                bullet.Speed = player.Weapon.Speed;
                bullet.Damage = player.Weapon.Damage;
            }
        }

        private bool CanDamage(Player player)
        {
            if (player.Weapon.Interval > 0f)
            {
                player.Weapon.Interval -= Time.deltaTime;
                return false;
            }

            for (int i = _levelService.Enemies.Count - 1; i >= 0; i--)
            {
                float sqrMagnitude = (player.Position - _levelService.Enemies[i].Position).sqrMagnitude;

                if (player.Weapon.RadiusSqr > sqrMagnitude)
                {
                    player.Weapon.Interval = _staticDataService.GetGameData().PlayerFireInterval;
                    return true;
                }
            }

            return false;
        }

        private int FindNearestTarget(Player player)
        {
            int index = 0;
            float minDistance = float.MaxValue;

            for (int i = _levelService.Enemies.Count - 1; i >= 0; i--)
            {
                float sqrMagnitude = (player.Position - _levelService.Enemies[i].Position).sqrMagnitude;

                if (minDistance > sqrMagnitude)
                {
                    index = i;
                }
            }

            return index;
        }
    }
}