using System.Runtime.CompilerServices;
using Game.Core.Implementation;
using Game.Infrastructure.InputService;
using VContainer;

namespace Game.Gameplay.Systems
{
    public sealed class ExecuteInputSystem : SystemBase
    {
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            _inputService.Execute();
        }
    }
}