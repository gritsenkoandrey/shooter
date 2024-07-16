using Infrastructure.StaticDataService;
using Infrastructure.StaticDataService.Data;
using JetBrains.Annotations;
using UI;
using UnityEngine;

namespace Infrastructure.Factories.ScreenFactory
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