using Gameplay.Components;
using UnityEngine;

namespace Infrastructure.Factories.GameFactory
{
    public interface IGameFactory
    {
        Level CreateLevel();
        Player CreatePlayer(Vector3 position, Quaternion rotation, Transform parent);
        Enemy CreateEnemy(Vector3 position, Quaternion rotation, Transform parent);
        Bullet CreateBullet(Vector3 position, Quaternion rotation, Transform parent);
    }
}