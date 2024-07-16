using System;
using System.Collections.Generic;
using Gameplay.Components;
using JetBrains.Annotations;

namespace Infrastructure.LevelService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class LevelService : ILevelService
    {
        private int _health;
        private int _kills;
        public List<Enemy> Enemies { get; } = new ();
        public float Finish { get; set; }
        public int Health
        {
            get => _health;
            
            set
            {
                _health = value;
                OnChangeHealth?.Invoke(value);
            }
        }
        public int Kills
        {
            get => _kills;
            
            set
            {
                _kills = value;
                OnChangeKills?.Invoke(value);
            }
        }

        public event Action<int> OnChangeHealth;

        public event Action<int> OnChangeKills;

        void ILevelService.CleanUp()
        {
            Enemies.Clear();
            Finish = 0f;
            Health = 0;
            Kills = 0;
        }
    }
}