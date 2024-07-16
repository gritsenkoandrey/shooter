using Core.Implementation;
using UnityEngine;

namespace Gameplay.Components
{
    public sealed class Bullet : EntityComponent<Bullet>
    {
        [SerializeField] private float _collisionRadius = 1f;
        
        public Vector3 Forward => transform.forward;
        public Vector3 Position => transform.position;
        public float CollisionRadius => _collisionRadius;
        public Vector3 Direction { get; set; }
        public int Damage { get; set; }
        public float Speed { get; set; }
    }
}