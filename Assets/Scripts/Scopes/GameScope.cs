using Scopes.EntryPoints;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public sealed class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<EntryPointGameSystem>(Lifetime.Scoped).AsSelf().Build();
        }
    }
}