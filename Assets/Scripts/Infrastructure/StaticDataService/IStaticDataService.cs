using Infrastructure.StaticDataService.Data;
using UI;

namespace Infrastructure.StaticDataService
{
    public interface IStaticDataService
    {
        void Load();
        ScreenData GetScreenData(ScreenType screenType);
        PrefabData GetPrefabData();
        GameData GetGameData();
    }
}