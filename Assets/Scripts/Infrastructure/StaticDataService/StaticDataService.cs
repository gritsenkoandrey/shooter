using System.Collections.Generic;
using System.Linq;
using Game.Infrastructure.AssetService;
using Game.Infrastructure.StaticDataService.Data;
using Game.Infrastructure.StaticDataService.DataPath;
using Game.UI;
using JetBrains.Annotations;

namespace Game.Infrastructure.StaticDataService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly IAssetService _assetService;
        
        private IDictionary<ScreenType, ScreenData> _screens;
        private PrefabData _prefabData;
        private GameData _gameData;
        private PoolData _poolData;

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
            _poolData = _assetService.LoadFromResources<PoolData>(AssetAddress.PoolDataPath);
        }

        ScreenData IStaticDataService.GetScreenData(ScreenType screenType) => 
            _screens.TryGetValue(screenType, out ScreenData staticData) ? staticData : null;
        PrefabData IStaticDataService.GetPrefabData() => _prefabData;
        GameData IStaticDataService.GetGameData() => _gameData;
        PoolData IStaticDataService.GetPoolData() => _poolData;
    }
}