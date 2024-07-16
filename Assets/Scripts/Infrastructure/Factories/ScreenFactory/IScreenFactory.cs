using UI;
using UnityEngine;

namespace Infrastructure.Factories.ScreenFactory
{
    public interface IScreenFactory
    {
        BaseScreen CreateScreen(ScreenType screenType, Transform parent);
    }
}