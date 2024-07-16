using Core.Implementation;
using UnityEngine;

namespace Gameplay.Components
{
    public sealed class Enemy : EntityComponent<Enemy>
    {
        [SerializeField] private float _collisionRadius = 3f;

        public Vector3 Position => transform.position;
        public float CollisionRadius => _collisionRadius;
        public float Speed { get; set; }
        public int Health { get; set; }
    }
}