using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Game.Gameplay.Jobs
{
    [BurstCompile]
    public struct BulletCollisionJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<CollisionStruct> BulletArray;
        [ReadOnly] public NativeArray<CollisionStruct> EnemyArray;
        [WriteOnly] public NativeArray<int> CollisionArray;

        void IJobParallelFor.Execute(int index)
        {
            for (int i = 0; i < EnemyArray.Length; i++)
            {
                float distance = math.distancesq(BulletArray[index].Position, EnemyArray[i].Position);
                float collisionDistance = BulletArray[index].Radius + EnemyArray[i].Radius;

                if (distance < collisionDistance)
                {
                    CollisionArray[index] = i;
                    
                    return;
                }
            }
        }
    }

    [BurstCompile]
    public struct CollisionStruct
    {
        public float3 Position;
        public float Radius;
    }
}