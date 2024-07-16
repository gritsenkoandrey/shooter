using System.Collections.Generic;
using System.Linq;
using Infrastructure.AssetService;
using Infrastructure.StaticDataService.Data;
using Infrastructure.StaticDataService.DataPath;
using JetBrains.Annotations;
using UI;

namespace Infrastructure.StaticDataService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly IAssetService _assetService;
        
        private IDictionary<ScreenType, ScreenData> _screens;
        private PrefabData _prefabData;
        private GameData _gameData;

        public StaticDataService(IAssetService assetService)
        {
            _assetService = assetService;
        }

        void IStaticDataService.Load()
        {
            _screens = _assetService
                .LoadAllFromResources<ScreenData>(AssetAddress.ScreenDataPath)
                .ToDictionary(data => data.ScreenType, data => data);

            _prefabData = _assetService.LoadFromResources<PrefabData>(AssetAddress.PrefabDataPath);
            _gameData = _assetService.LoadFromResources<GameData>(AssetAddress.GameDataPath);
        }

        ScreenData IStaticDataService.GetScreenData(ScreenType screenType) => 
            _screens.TryGetValue(screenType, out ScreenData staticData) ? staticData : null;
        PrefabData IStaticDataService.GetPrefabData() => _prefabData;
        GameData IStaticDataService.GetGameData() => _gameData;
    }
}