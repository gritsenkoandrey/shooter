using Core.Implementation;
using UnityEngine;

namespace Gameplay.Components
{
    public sealed class Level : EntityComponent<Level>
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _finishLine;

        public Transform PlayerSpawnPoint => _playerSpawnPoint;
        public Transform FinishLine => _finishLine;
    }
}