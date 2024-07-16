using Gameplay.Components;
using UnityEngine;

namespace Infrastructure.StaticDataService.Data
{
    [CreateAssetMenu(fileName = nameof(PrefabData), menuName = "Data/" + nameof(PrefabData))]
    public sealed class PrefabData : ScriptableObject
    {
        public Level Level;
        public Player Player;
        public Enemy Enemy;
        public Bullet Bullet;
    }
}