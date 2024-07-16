using Core.Implementation;
using UnityEngine;

namespace Gameplay.Components
{
    public sealed class Player : EntityComponent<Player>
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _collisionRadius = 3f;

        public Weapon Weapon => _weapon;
        public Vector3 Position => transform.position;
        public float CollisionRadius => _collisionRadius;
        public float Speed { get; set; }
    }
}