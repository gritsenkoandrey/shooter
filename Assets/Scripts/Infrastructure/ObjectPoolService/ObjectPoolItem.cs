using UnityEngine;

namespace Game.Infrastructure.ObjectPoolService
{
    public struct ObjectPoolItem
    {
        public readonly GameObject Prefab;
        public readonly int Count;

        public ObjectPoolItem(GameObject prefab, int count)
        {
            Prefab = prefab;
            Count = count;
        }
    }
}