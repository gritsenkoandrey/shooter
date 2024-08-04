using Game.Gameplay.Jobs;
using JetBrains.Annotations;
using Unity.Jobs;
using UnityEngine.Jobs;

namespace Game.Infrastructure.JobService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class JobService : IJobService
    {
        public void Init()
        {
            IJobParallelForExtensions.EarlyJobInit<BulletCollisionJob>();
            IJobParallelForTransformExtensions.EarlyJobInit<BulletMovementJob>();
            IJobParallelForTransformExtensions.EarlyJobInit<EnemyMovementJob>();
        }
    }
}