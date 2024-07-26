using Game.UI;
using UnityEngine;

namespace Game.Infrastructure.Factories.ScreenFactory
{
    public interface IScreenFactory
    {
        BaseScreen CreateScreen(ScreenType screenType, Transform parent);
    }
}