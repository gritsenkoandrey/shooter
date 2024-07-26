using System;

namespace Game.Core.Implementation
{
    public interface ISystem : IDisposable
    {
        void EnableSystem();
        void DisableSystem();
        void Update();
        void FixedUpdate();
        void LateUpdate();
    }
}