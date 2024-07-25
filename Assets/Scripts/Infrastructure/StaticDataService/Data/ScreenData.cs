using UI;
using UnityEngine;

namespace Infrastructure.StaticDataService.Data
{
    [CreateAssetMenu(fileName = nameof(ScreenData), menuName = "Data/" + nameof(ScreenData))]
    public sealed class ScreenData : ScriptableObject
    {
        public ScreenType ScreenType;
        public GameObject Prefab;
    }
}