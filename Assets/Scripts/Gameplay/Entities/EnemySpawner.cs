using Game.Core.Implementation;
using UnityEngine;

namespace Game.Gameplay.Entities
{
    public sealed class EnemySpawner : EntityComponent<EnemySpawner>
    {
        [SerializeField] private Transform[] _points;

        public Transform[] Points => _points;
        public float SpawnDelay { get; set; }
    }
}