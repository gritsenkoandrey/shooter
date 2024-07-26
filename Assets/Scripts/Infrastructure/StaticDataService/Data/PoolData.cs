using Game.Infrastructure.ObjectPoolService;
using UnityEngine;

namespace Game.Infrastructure.StaticDataService.Data
{
    [CreateAssetMenu(fileName = nameof(PoolData), menuName = "Data/" + nameof(PoolData))]
    public sealed class PoolData : ScriptableObject
    {
        public bool LogStatus;
        
        public ObjectPoolData[] PoolItems;
    }
}