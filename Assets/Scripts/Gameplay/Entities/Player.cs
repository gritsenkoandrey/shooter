using Game.Core.Implementation;
using Game.Gameplay.Components;
using Game.Gameplay.Implementation;
using UnityEngine;

namespace Game.Gameplay.Entities
{
    public sealed class Player : EntityComponent<Player>, ICollision
    {
        [SerializeField] private float _radius = 3f;

        public float Radius => _radius;
        public Vector3 Position => transform.position;
        public Weapon Weapon { get; } = new ();
        public Move Move { get; } = new ();
    }
}