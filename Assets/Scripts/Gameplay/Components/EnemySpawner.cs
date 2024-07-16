using Core.Implementation;
using UnityEngine;

namespace Gameplay.Components
{
    public sealed class EnemySpawner : EntityComponent<EnemySpawner>
    {
        [SerializeField] private Transform[] _points;

        public Transform[] Points => _points;
        public float SpawnDelay { get; set; }
    }
}