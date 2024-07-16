using System;
using System.Collections.Generic;
using Gameplay.Components;

namespace Infrastructure.LevelService
{
    public interface ILevelService
    {
        List<Enemy> Enemies { get; }
        public int Health { get; set; }
        public int Kills { get; set; }
        public float Finish { get; set; }
        public event Action<int> OnChangeHealth;
        public event Action<int> OnChangeKills;
        void CleanUp();
    }
}