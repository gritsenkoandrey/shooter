using System.Collections.Generic;
using Game.Gameplay.Entities;

namespace Game.Infrastructure.LevelService
{
    public interface ILevelService
    {
        IReadOnlyList<Enemy> Enemies { get; }
        void AddEnemy(Enemy enemy);
        void RemoveEnemy(Enemy enemy);
        void RemoveEnemy(int index);
        void CleanUp();
    }
}