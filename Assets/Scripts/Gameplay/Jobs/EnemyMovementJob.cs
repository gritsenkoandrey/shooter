using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace Game.Gameplay.Jobs
{
    [BurstCompile]
    public struct EnemyMovementJob : IJobParallelForTransform
    {
        [ReadOnly] public NativeArray<float> SpeedArray;
        [ReadOnly] public float DeltaTime;
        [ReadOnly] public float3 Direction;
        
        void IJobParallelForTransform.Execute(int index, TransformAccess transform)
        {
            float3 position = transform.position;
            position += Direction * SpeedArray[index] * DeltaTime;
            transform.position = position;
        }
    }
}