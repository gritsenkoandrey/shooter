using Game.Core.Implementation;
using UnityEngine;

namespace Game.Gameplay.Entities
{
    public sealed class Level : EntityComponent<Level>
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _boundaryLine;

        public Transform PlayerSpawnPoint => _playerSpawnPoint;
        public float BoundaryLine => _boundaryLine.position.y;
    }
}