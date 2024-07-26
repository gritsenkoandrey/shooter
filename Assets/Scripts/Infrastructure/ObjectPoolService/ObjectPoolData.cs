using UnityEngine;

namespace Game.Infrastructure.ObjectPoolService
{
    [System.Serializable]
    public struct ObjectPoolData
    {
        public GameObject Prefab;
        public int Count;
    }
}