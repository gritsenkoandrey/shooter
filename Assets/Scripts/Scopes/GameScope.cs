using Game.Gameplay.Models;
using Game.Scopes.EntryPoints;
using VContainer;
using VContainer.Unity;

namespace Game.Scopes
{
    public sealed class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.Register<PlayerHealth>(Lifetime.Scoped).As<IInitializable>().AsSelf();
            builder.Register<EnemyKillCounter>(Lifetime.Scoped).As<IInitializable>().AsSelf();
            builder.Register<LevelBounds>(Lifetime.Scoped).As<IInitializable>().AsSelf();

            builder.RegisterEntryPoint<EntryPointGameSystem>(Lifetime.Scoped).AsSelf().Build();
        }
    }
}