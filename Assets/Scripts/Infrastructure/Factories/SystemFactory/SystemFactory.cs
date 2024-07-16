using Core.Implementation;
using Gameplay.Systems;
using JetBrains.Annotations;
using Utils;
using VContainer;

namespace Infrastructure.Factories.SystemFactory
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SystemFactory : ISystemFactory
    {
        private readonly IObjectResolver _objectResolver;

        public SystemFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        ISystem[] ISystemFactory.CreateGameSystems()
        {
            ISystem[] systems =
            {
                new InputSystem(),
                new PlayerMovementSystem(),
                new PlayerDamageSystem(),
                new PlayerHealthViewSystem(),
                new EnemyMovementSystem(),
                new EnemyDamageSystem(),
                new EnemySpawnSystem(),
                new BulletMovementSystem(),
                new BulletDamageSystem(),
            };
            
            systems.Foreach(_objectResolver.Inject);

            return systems;
        }
    }
}