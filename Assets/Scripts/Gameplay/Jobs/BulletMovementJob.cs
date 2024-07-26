using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace Game.Gameplay.Jobs
{
    [BurstCompile]
    public struct BulletMovementJob : IJobParallelForTransform
    {
        [ReadOnly] public NativeArray<float3> DirectionArray;
        [ReadOnly] public NativeArray<float> SpeedArray;
        [ReadOnly] public float DeltaTime;

        public void Execute(int index, TransformAccess transform)
        {
            float3 position = transform.position;
            position += DirectionArray[index] * SpeedArray[index] * DeltaTime;
            transform.position = position;
        }
    }
}