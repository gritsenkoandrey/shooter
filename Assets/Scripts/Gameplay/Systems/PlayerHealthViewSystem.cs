using Core.Implementation;
using Gameplay.Components;
using Infrastructure.LevelService;
using VContainer;

namespace Gameplay.Systems
{
    public sealed class PlayerHealthViewSystem : SystemComponent<PlayerHealthView>
    {
        private ILevelService _levelService;

        [Inject]
        private void Construct(ILevelService levelService)
        {
            _levelService = levelService;
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
            
            _levelService.OnChangeHealth += OnChangeHealth;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            _levelService.OnChangeHealth -= OnChangeHealth;
        }

        protected override void OnEnableComponent(PlayerHealthView component)
        {
            base.OnEnableComponent(component);
            
            component.Text.text = $"Health: {_levelService.Health}";
        }

        private void OnChangeHealth(int health)
        {
            foreach (PlayerHealthView entity in Entities)
            {
                entity.Text.text = $"Health: {health}";
            }
        }
    }
}