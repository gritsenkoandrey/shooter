using System.Runtime.CompilerServices;
using Game.Core.Implementation;
using Game.Gameplay.Entities;
using Game.Gameplay.Models;
using Game.Utils;
using VContainer;

namespace Game.Gameplay.Systems
{
    public sealed class PlayerHealthViewSystem : SystemComponent<PlayerHealthView>
    {
        private PlayerHealth _playerHealth;

        private const string HealthFormat = "Health: {0}";

        [Inject]
        private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
            
            _playerHealth.OnChangeHealth += OnChangeHealth;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            _playerHealth.OnChangeHealth -= OnChangeHealth;
        }

        protected override void OnEnableComponent(PlayerHealthView component)
        {
            base.OnEnableComponent(component);
            
            component.Text.text = string.Format(HealthFormat, _playerHealth.Health.ToString());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnChangeHealth(int health)
        {
            Entities.Foreach(entity => entity.Text.text = string.Format(HealthFormat, health.ToString()));
        }
    }
}