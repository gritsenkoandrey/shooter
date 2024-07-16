using System;
using VContainer.Unity;

namespace Scopes.EntryPoints
{
    public interface IEntryPointSystem : IInitializable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
    }
}