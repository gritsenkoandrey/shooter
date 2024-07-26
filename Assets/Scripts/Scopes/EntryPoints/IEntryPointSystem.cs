using System;
using VContainer.Unity;

namespace Game.Scopes.EntryPoints
{
    public interface IEntryPointSystem : IInitializable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
    }
}