using Game.Gameplay.Entities;
using Game.Gameplay.Models;
using Game.Infrastructure.LevelService;
using Game.Infrastructure.ObjectPoolService;
using Game.Infrastructure.StaticDataService;
using Game.Infrastructure.StaticDataService.Data;
using Game.Scopes;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.Factories.GameFactory
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelService _levelService;
        private readonly IObjectPoolService _objectPoolService;

        public GameFactory(IStaticDataService staticDataService, ILevelService levelService, IObjectPoolService objectPoolService)
        {
            _staticDataService = staticDataService;
            _levelService = levelService;
            _objectPoolService = objectPoolService;
        }
        
        Level IGameFactory.CreateLevel()
        {
            Level level = Object.Instantiate(_staticDataService.GetPrefabData().Level);

            IObjectResolver container = LifetimeScope.Find<GameScope>().Container;

            container.Resolve<LevelBounds>().SetBoundaryLine(level.BoundaryLine);

            return level;
        }

        Player IGameFactory.CreatePlayer(Vector3 position, Quaternion rotation, Transform parent)
        {
            Player player = Object.Instantiate(_staticDataService.GetPrefabData().Player, position, rotation, parent);

            GameData data = _staticDataService.GetGameData();

            player.Move.SetSpeed(data.PlayerSpeed);
            player.Weapon.SetFireRadius(Mathf.Pow(data.PlayerFireRadius, 2));
            player.Weapon.SetFireInterval(data.PlayerFireInterval);
            player.Weapon.SetBulletSpeed(data.PlayerBulletSpeed);
            player.Weapon.SetBulletDamage(data.PlayerFireDamage);
            
            return player;
        }

        Enemy IGameFactory.CreateEnemy(Vector3 position, Quaternion rotation)
        {
            Enemy enemy = _objectPoolService
                .SpawnObject(_staticDataService.GetPrefabData().Enemy, position, rotation)
                .GetComponent<Enemy>();
            
            GameData data = _staticDataService.GetGameData();

            enemy.Move.SetSpeed(Random.Range(data.EnemySpeed.Min, data.EnemySpeed.Max));
            enemy.Health.CurrentHealth = data.EnemyHealth;
            
            _levelService.AddEnemy(enemy);

            return enemy;
        }

        Bullet IGameFactory.CreateBullet(Vector3 position, Quaternion rotation)
        {
            return _objectPoolService
                .SpawnObject(_staticDataService.GetPrefabData().Bullet, position, rotation)
                .GetComponent<Bullet>();
        }
    }
}