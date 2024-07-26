using System.Collections.Generic;
using Game.Gameplay.Entities;
using JetBrains.Annotations;

namespace Game.Infrastructure.LevelService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class LevelService : ILevelService
    {
        private readonly List<Enemy> _enemies = new ();

        IReadOnlyList<Enemy> ILevelService.Enemies => _enemies;
        void ILevelService.AddEnemy(Enemy enemy) => _enemies.Add(enemy);
        void ILevelService.RemoveEnemy(Enemy enemy) => _enemies.Remove(enemy);
        void ILevelService.CleanUp() => _enemies.Clear();
    }
}