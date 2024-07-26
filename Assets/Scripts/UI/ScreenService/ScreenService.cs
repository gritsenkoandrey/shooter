using System.Collections.Generic;
using Game.Infrastructure.Factories.ScreenFactory;
using UnityEngine;
using VContainer;

namespace Game.UI.ScreenService
{
    public sealed class ScreenService : MonoBehaviour, IScreenService
    {
        [SerializeField] private BaseCanvas _baseCanvas;

        private readonly Stack<BaseScreen> _screens = new();

        private IScreenFactory _screenFactory;

        [Inject]
        private void Construct(IScreenFactory screenFactory)
        {
            _screenFactory = screenFactory;
        }
        
        BaseScreen IScreenService.CreateScreen(ScreenType screenType)
        {
            BaseScreen screen = _screenFactory.CreateScreen(screenType, _baseCanvas.Root);
            
            if (_screens.TryPop(out BaseScreen oldScreen))
            {
                Destroy(oldScreen.gameObject);
            }
            
            _screens.Push(screen);
            
            return screen;
        }

        void IScreenService.CleanUp()
        {
            foreach (BaseScreen screen in _screens)
            {
                Destroy(screen.gameObject);
            }
            
            _screens.Clear();
        }
    }
}