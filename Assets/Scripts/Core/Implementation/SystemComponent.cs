using System.Collections.Generic;

namespace Core.Implementation
{
    public abstract class SystemComponent<T> : SystemBase where T : Entity
    {
        private readonly List<T> _entities;

        protected IReadOnlyList<T> Entities => _entities;

        protected SystemComponent() => _entities = new List<T>();

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
            
            EntityBridge<T>.OnRegistered += OnEnableComponent;
            EntityBridge<T>.OnUnregistered += OnDisableComponent;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            EntityBridge<T>.OnRegistered -= OnEnableComponent;
            EntityBridge<T>.OnUnregistered -= OnDisableComponent;
        }

        protected virtual void OnEnableComponent(T component)
        {
            if (_entities.Contains(component))
            {
                return;
            }
            
            _entities.Add(component);
        }

        protected virtual void OnDisableComponent(T component)
        {
            _entities.Remove(component);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _entities.Clear();
        }
    }
}