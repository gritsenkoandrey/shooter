using System.Linq;
using System.Runtime.CompilerServices;
using Game.Core.Implementation;
using Game.Gameplay.Entities;
using Game.Gameplay.Jobs;
using Game.Gameplay.Models;
using Game.Infrastructure.ObjectPoolService;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;
using VContainer;

namespace Game.Gameplay.Systems
{
    public sealed class BulletMovementSystem : SystemComponent<Bullet>
    {
        private IObjectPoolService _objectPoolService;
        private LevelBounds _levelBounds;

        [Inject]
        private void Construct(IObjectPoolService objectPoolService, LevelBounds levelBounds)
        {
            _objectPoolService = objectPoolService;
            _levelBounds = levelBounds;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Move();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Move()
        {
            NativeArray<float3> directionArray = new NativeArray<float3>(Entities.Count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            NativeArray<float> speedArray = new NativeArray<float>(Entities.Count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            TransformAccessArray transformArray = new TransformAccessArray(Entities.Select(entity => entity.transform).ToArray());
            
            for (int i = 0; i < Entities.Count; i++)
            {
                directionArray[i] = Entities[i].Direction;
                speedArray[i] = Entities[i].Speed;
            }

            BulletMovementJob job = new BulletMovementJob
            {
                DirectionArray = directionArray,
                SpeedArray = speedArray,
                DeltaTime = Time.deltaTime
            };
            
            JobHandle handle = job.Schedule(transformArray);
            
            handle.Complete();

            for (int i = Entities.Count - 1; i >= 0; i--)
            {
                if (_levelBounds.ScreenBounds.x < Mathf.Abs(Entities[i].Position.x) || _levelBounds.ScreenBounds.y < Mathf.Abs(Entities[i].Position.y))
                {
                    _objectPoolService.ReleaseObject(Entities[i].gameObject);
                }
            }

            directionArray.Dispose();
            speedArray.Dispose();
            transformArray.Dispose();
        }
    }
}