using System.Runtime.CompilerServices;
using Game.Core.Implementation;
using Game.Infrastructure.ObjectPoolService;
using VContainer;

namespace Game.Gameplay.Systems
{
    public sealed class ExecuteObjectPoolSystem : SystemBase
    {
        private IObjectPoolService _objectPoolService;

        [Inject]
        private void Construct(IObjectPoolService objectPoolService)
        {
            _objectPoolService = objectPoolService;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            _objectPoolService.ReleaseAll();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            _objectPoolService.Execute();
        }
    }
}