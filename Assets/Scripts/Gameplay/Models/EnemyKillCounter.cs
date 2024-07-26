using System;
using Game.Infrastructure.StaticDataService;
using Game.Utils;
using JetBrains.Annotations;
using VContainer.Unity;

namespace Game.Gameplay.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class EnemyKillCounter : IInitializable
    {
        private readonly IStaticDataService _staticDataService;
                
        private int _kills;

        public EnemyKillCounter(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        void IInitializable.Initialize()
        {
            MinMaxInt requiredKills = _staticDataService.GetGameData().RequiredKills;
            
            Kills = UnityEngine.Random.Range(requiredKills.Min, requiredKills.Max);
        }

        public int Kills
        {
            get => _kills;
            
            set
            {
                _kills = value;
                
                OnChangeKills?.Invoke(_kills);
            }
        }

        public event Action<int> OnChangeKills;
    }
}