using Game.Core.Implementation;
using Game.Gameplay.Implementation;
using UnityEngine;

namespace Game.Gameplay.Entities
{
    public sealed class Bullet : EntityComponent<Bullet>, ICollision
    {
        [SerializeField] private float _radius = 1f;
        
        public Vector3 Position => transform.position;
        public float Radius => _radius;
        public Vector3 Direction { get; private set; }
        public int Damage { get; private set; }
        public float Speed { get; private set; }

        public void SetDirection(Vector3 direction) => Direction = direction;
        public void SetDamage(int damage) => Damage = damage;
        public void SetSpeed(float speed) => Speed = speed;
    }
}