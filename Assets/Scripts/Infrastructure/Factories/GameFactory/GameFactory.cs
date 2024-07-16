using Gameplay.Components;
using Infrastructure.LevelService;
using Infrastructure.StaticDataService;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Factories.GameFactory
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelService _levelService;

        public GameFactory(IStaticDataService staticDataService, ILevelService levelService)
        {
            _staticDataService = staticDataService;
            _levelService = levelService;
        }
        
        Level IGameFactory.CreateLevel()
        {
            Level level = Object.Instantiate(_staticDataService.GetPrefabData().Level);

            _levelService.Finish = level.FinishLine.position.y;
            
            return level;
        }

        Player IGameFactory.CreatePlayer(Vector3 position, Quaternion rotation, Transform parent)
        {
            Player player = Object.Instantiate(_staticDataService.GetPrefabData().Player, position, rotation, parent);

            player.Speed = _staticDataService.GetGameData().PlayerSpeed;
            player.Weapon.RadiusSqr = Mathf.Pow(_staticDataService.GetGameData().PlayerFireRadius, 2);
            player.Weapon.Interval = _staticDataService.GetGameData().PlayerFireInterval;
            player.Weapon.Speed = _staticDataService.GetGameData().PlayerBulletSpeed;
            player.Weapon.Damage = _staticDataService.GetGameData().PlayerFireDamage;
            
            _levelService.Health = _staticDataService.GetGameData().PlayerHealth;
            _levelService.Kills = Random.Range(_staticDataService.GetGameData().RequiredKills.Min, _staticDataService.GetGameData().RequiredKills.Max);
            
            return player;
        }

        Enemy IGameFactory.CreateEnemy(Vector3 position, Quaternion rotation, Transform parent)
        {
            Enemy enemy = Object.Instantiate(_staticDataService.GetPrefabData().Enemy, position, rotation, parent);
            
            enemy.Speed = Random.Range(_staticDataService.GetGameData().EnemySpeed.Min, _staticDataService.GetGameData().EnemySpeed.Max);
            enemy.Health = _staticDataService.GetGameData().EnemyHealth;
            
            _levelService.Enemies.Add(enemy);

            return enemy;
        }

        Bullet IGameFactory.CreateBullet(Vector3 position, Quaternion rotation, Transform parent)
        {
            return Object.Instantiate(_staticDataService.GetPrefabData().Bullet, position, rotation, parent);
        }
    }
}