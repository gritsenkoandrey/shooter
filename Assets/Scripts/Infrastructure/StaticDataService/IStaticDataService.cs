using Game.Infrastructure.StaticDataService.Data;
using Game.UI;

namespace Game.Infrastructure.StaticDataService
{
    public interface IStaticDataService
    {
        void Load();
        ScreenData GetScreenData(ScreenType screenType);
        PrefabData GetPrefabData();
        GameData GetGameData();
        PoolData GetPoolData();
    }
}