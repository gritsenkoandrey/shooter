using Game.Gameplay.Entities;
using UnityEngine;

namespace Game.Infrastructure.Factories.GameFactory
{
    public interface IGameFactory
    {
        Level CreateLevel();
        Player CreatePlayer(Vector3 position, Quaternion rotation, Transform parent);
        Enemy CreateEnemy(Vector3 position, Quaternion rotation);
        Bullet CreateBullet(Vector3 position, Quaternion rotation);
    }
}