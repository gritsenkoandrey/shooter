using Core.Implementation;
using Gameplay.Components;
using Infrastructure.Factories.GameFactory;
using Infrastructure.StaticDataService;
using UnityEngine;
using Utils;
using VContainer;

namespace Gameplay.Systems
{
    public sealed class EnemySpawnSystem : SystemComponent<EnemySpawner>
    {
        private IGameFactory _gameFactory;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }
        
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Spawn);
        }

        private void Spawn(EnemySpawner enemySpawner)
        {
            enemySpawner.SpawnDelay -= Time.deltaTime;

            if (enemySpawner.SpawnDelay < 0f)
            {
                enemySpawner.SpawnDelay = Random.Range(_staticDataService.GetGameData().EnemySpawnDelay.Min, _staticDataService.GetGameData().EnemySpawnDelay.Max);
                
                Vector3 position = enemySpawner.Points[Random.Range(0, enemySpawner.Points.Length)].position;

                _gameFactory.CreateEnemy(position, Quaternion.identity, enemySpawner.transform.parent);
            }
        }
    }
}