using System;

namespace Core.Implementation
{
    public abstract class SystemBase : ISystem
    {
        void ISystem.EnableSystem() => OnEnableSystem();
        void ISystem.DisableSystem() => OnDisableSystem();
        void ISystem.Update() => OnUpdate();
        void ISystem.FixedUpdate() => OnFixedUpdate();
        void ISystem.LateUpdate() => OnLateUpdate();
        void IDisposable.Dispose() => OnDispose();

        protected virtual void OnEnableSystem() { }
        protected virtual void OnDisableSystem() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnFixedUpdate() { }
        protected virtual void OnLateUpdate() { }
        protected virtual void OnDispose() { }
    }
}