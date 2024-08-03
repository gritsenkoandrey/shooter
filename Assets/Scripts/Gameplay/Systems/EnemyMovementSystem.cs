using System.Linq;
using System.Runtime.CompilerServices;
using Game.Core.Implementation;
using Game.Gameplay.Entities;
using Game.Gameplay.Jobs;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Game.Gameplay.Systems
{
    public sealed class EnemyMovementSystem : SystemComponent<Enemy>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Move();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Move()
        {
            NativeArray<float> speedArray = new NativeArray<float>(Entities.Count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            TransformAccessArray transformArray = new TransformAccessArray(Entities.Select(entity => entity.transform).ToArray());
            
            for (int i = 0; i < Entities.Count; i++)
            {
                speedArray[i] = Entities[i].Move.Speed;
            }

            EnemyMovementJob job = new EnemyMovementJob
            {
                SpeedArray = speedArray,
                DeltaTime = Time.deltaTime,
                Direction = Vector3.down
            };

            JobHandle handle = job.Schedule(transformArray);
            
            handle.Complete();

            speedArray.Dispose();
            transformArray.Dispose();
        }
    }
}