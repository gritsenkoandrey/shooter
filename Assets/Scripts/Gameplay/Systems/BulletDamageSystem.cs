using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Game.Core.Implementation;
using Game.Gameplay.Entities;
using Game.Gameplay.Jobs;
using Game.Infrastructure.LevelService;
using Game.Infrastructure.ObjectPoolService;
using Unity.Collections;
using Unity.Jobs;
using VContainer;

namespace Game.Gameplay.Systems
{
    public sealed class BulletDamageSystem : SystemComponent<Bullet>
    {
        private ILevelService _levelService;
        private IObjectPoolService _objectPoolService;

        [Inject]
        private void Construct(ILevelService levelService, IObjectPoolService objectPoolService)
        {
            _levelService = levelService;
            _objectPoolService = objectPoolService;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Collision(_levelService.Enemies);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Collision(IReadOnlyList<Enemy> enemies)
        {
            int enemyCount = enemies.Count;
            int bulletCount = Entities.Count;
            
            NativeArray<CollisionStruct> bulletArray = new NativeArray<CollisionStruct>(bulletCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            NativeArray<CollisionStruct> enemyArray = new NativeArray<CollisionStruct>(enemyCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            NativeArray<int> collisionArray = new NativeArray<int>(bulletCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

            for (int i = 0; i < bulletCount; i++)
            {
                collisionArray[i] = -1;
                
                bulletArray[i] = new CollisionStruct
                {
                    Position = Entities[i].Position,
                    Radius = Entities[i].Radius
                };
            }

            for (int i = 0; i < enemyCount; i++)
            {
                enemyArray[i] = new CollisionStruct
                {
                    Position = enemies[i].Position,
                    Radius = enemies[i].Radius
                };
            }

            BulletCollisionJob job = new BulletCollisionJob
            {
                BulletArray = bulletArray,
                EnemyArray = enemyArray,
                CollisionArray = collisionArray,
            };

            JobHandle handle = job.Schedule(bulletCount, 64);
            
            handle.Complete();

            for (int i = bulletCount - 1; i >= 0; i--)
            {
                if (collisionArray[i] > -1)
                {
                    int target = collisionArray[i];

                    enemies[target].Health.CurrentHealth -= Entities[i].Damage;
                    
                    _objectPoolService.ReleaseObject(Entities[i].gameObject);
                }
            }

            bulletArray.Dispose();
            enemyArray.Dispose();
            collisionArray.Dispose();
        }
    }
}