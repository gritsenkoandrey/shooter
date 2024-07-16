using System;
using Core.Implementation;
using Infrastructure.Factories.SystemFactory;
using JetBrains.Annotations;
using Utils;
using VContainer.Unity;

namespace Scopes.EntryPoints
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class EntryPointGameSystem : IEntryPointSystem
    {
        private readonly ISystemFactory _systemFactory;
        
        private ISystem[] _systems = Array.Empty<ISystem>();

        public EntryPointGameSystem(ISystemFactory systemFactory)
        {
            _systemFactory = systemFactory;
        }
        
        void IInitializable.Initialize()
        {
            _systems = _systemFactory.CreateGameSystems();
            _systems.Foreach(Enable);
        }
        
        void ITickable.Tick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].Update();
            }
        }
        void IFixedTickable.FixedTick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].FixedUpdate();
            }
        }
        void ILateTickable.LateTick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].LateUpdate();
            }
        }
        
        void IDisposable.Dispose()
        {
            _systems.Foreach(Disable);
            _systems = Array.Empty<ISystem>();
        }

        private void Enable(ISystem system)
        {
            system.EnableSystem();
        }

        private void Disable(ISystem system)
        {
            system.DisableSystem();
            system.Dispose();
        }
    }
}