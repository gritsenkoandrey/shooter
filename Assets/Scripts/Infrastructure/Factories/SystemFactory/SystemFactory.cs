using Game.Core.Implementation;
using Game.Gameplay.Systems;
using JetBrains.Annotations;

namespace Game.Infrastructure.Factories.SystemFactory
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SystemFactory : ISystemFactory
    {
        ISystem[] ISystemFactory.CreateGameSystems()
        {
            ISystem[] systems =
            {
                new ExecuteInputSystem(),
                new ExecuteObjectPoolSystem(),
                new PlayerMovementSystem(),
                new PlayerShootingSystem(),
                new PlayerHealthViewSystem(),
                new EnemyMovementSystem(),
                new EnemyLifeTimeSystem(),
                new EnemySpawnSystem(),
                new BulletMovementSystem(),
                new BulletDamageSystem(),
            };

            return systems;
        }
    }
}