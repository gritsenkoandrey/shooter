using System;
using Game.Infrastructure.StaticDataService;
using JetBrains.Annotations;
using VContainer.Unity;

namespace Game.Gameplay.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class PlayerHealth : IInitializable
    {
        private readonly IStaticDataService _staticDataService;

        private int _health;

        public PlayerHealth(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        void IInitializable.Initialize()
        {
            Health = _staticDataService.GetGameData().PlayerHealth;
        }

        public int Health
        {
            get => _health;
            
            set
            {
                _health = value;

                if (_health < 0)
                {
                    _health = 0;
                }
                
                OnChangeHealth?.Invoke(_health);
            }
        }

        public event Action<int> OnChangeHealth;
    }
}