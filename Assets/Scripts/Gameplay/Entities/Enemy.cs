using Game.Core.Implementation;
using Game.Gameplay.Components;
using Game.Gameplay.Implementation;
using UnityEngine;

namespace Game.Gameplay.Entities
{
    public sealed class Enemy : EntityComponent<Enemy>, ICollision
    {
        [SerializeField] private float _radius = 3f;

        public Vector3 Position => transform.position;
        public float Radius => _radius;
        public Move Move { get; } = new ();
        public Health Health { get; } = new ();
    }
}