using Game.Infrastructure.StaticDataService;
using Game.Infrastructure.StaticDataService.Data;
using Game.UI;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Infrastructure.Factories.ScreenFactory
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class ScreenFactory : IScreenFactory
    {
        private readonly IStaticDataService _staticDataService;

        public ScreenFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        BaseScreen IScreenFactory.CreateScreen(ScreenType screenType, Transform parent)
        {
            ScreenData data = _staticDataService.GetScreenData(screenType);
            BaseScreen screen = Object.Instantiate(data.Prefab, parent).GetComponent<BaseScreen>();
            return screen;
        }
    }
}