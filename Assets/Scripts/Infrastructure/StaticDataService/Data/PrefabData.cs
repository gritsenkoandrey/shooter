using Game.Gameplay.Entities;
using UnityEngine;

namespace Game.Infrastructure.StaticDataService.Data
{
    [CreateAssetMenu(fileName = nameof(PrefabData), menuName = "Data/" + nameof(PrefabData))]
    public sealed class PrefabData : ScriptableObject
    {
        public Level Level;
        public Player Player;
        public GameObject Enemy;
        public GameObject Bullet;
    }
}