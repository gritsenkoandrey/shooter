using Core.Implementation;
using Infrastructure.InputService;
using VContainer;

namespace Gameplay.Systems
{
    public sealed class ExecuteInputSystem : SystemBase
    {
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            _inputService.Execute();
        }
    }
}