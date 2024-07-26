using UnityEngine;

namespace Game.Infrastructure.ObjectPoolService
{
    public interface IObjectPoolService
    {
        void Init();
        void Execute();
        GameObject SpawnObject(GameObject prefab);
        GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation);
        void ReleaseObject(GameObject clone);
        void ReleaseObjectAfterTime(GameObject clone, float time);
        void ReleaseAll();
    }
}