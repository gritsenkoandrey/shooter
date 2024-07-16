using Core.Implementation;
using Gameplay.Components;
using UnityEngine;
using Utils;

namespace Gameplay.Systems
{
    public sealed class EnemyMovementSystem : SystemComponent<Enemy>
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Move);
        }

        private void Move(Enemy enemy)
        {
            enemy.transform.position += Vector3.down * enemy.Speed * Time.deltaTime;
        }
    }
}