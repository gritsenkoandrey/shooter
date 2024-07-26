using Game.Utils;
using UnityEngine;

namespace Game.Infrastructure.StaticDataService.Data
{
    [CreateAssetMenu(fileName = nameof(GameData), menuName = "Data/" + nameof(GameData))]
    public sealed class GameData : ScriptableObject
    {
        public float PlayerFireRadius;
        public float PlayerFireInterval;
        public float PlayerBulletSpeed;
        public float PlayerSpeed;
        public int PlayerFireDamage;
        public int PlayerHealth;
        public MinMaxInt RequiredKills;
        public MinMaxFloat EnemySpawnDelay;
        public MinMaxFloat EnemySpeed;
        public int EnemyHealth;
    }
}