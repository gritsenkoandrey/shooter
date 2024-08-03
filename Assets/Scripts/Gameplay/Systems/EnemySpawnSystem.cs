using System.Runtime.CompilerServices;
using Game.Core.Implementation;
using Game.Gameplay.Entities;
using Game.Infrastructure.Factories.GameFactory;
using Game.Infrastructure.StaticDataService;
using Game.Utils;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Systems
{
    public sealed class EnemySpawnSystem : SystemComponent<EnemySpawner>
    {
        private IGameFactory _gameFactory;
        private IStaticDataService _staticDataService;

        private float _min;
        private float _max;

        [Inject]
        private void Construct(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _min = _staticDataService.GetGameData().EnemySpawnDelay.Min;
            _max = _staticDataService.GetGameData().EnemySpawnDelay.Max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Execute);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Execute(EnemySpawner component)
        {
            component.SpawnDelay -= Time.deltaTime;

            if (component.SpawnDelay < 0f)
            {
                component.SpawnDelay = Random.Range(_min, _max);
                
                Vector3 position = component.Points[Random.Range(0, component.Points.Length)].position;
                
                float offset = Random.Range(-2.5f, 2.5f);

                _gameFactory.CreateEnemy(position.AddX(offset), Quaternion.identity);
            }
        }
    }
}