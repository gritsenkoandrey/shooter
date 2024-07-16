using Gameplay.Components;
using Infrastructure.AssetService;
using Infrastructure.Factories.GameFactory;
using Infrastructure.LevelService;
using Infrastructure.LoadingScreenService;
using Infrastructure.SceneLoadService;
using UI.ScreenService;
using UnityEngine;
using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class LoadLevelState : IEnterLoadState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private ISceneLoadService _sceneLoadService;
        private IAssetService _assetService;
        private IScreenService _screenService;
        private IGameFactory _gameFactory;
        private ILevelService _levelService;
        private ILoadingScreenService _loadingScreenService;

        public LoadLevelState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(ISceneLoadService sceneLoadService, IAssetService assetService, IScreenService screenService, 
            IGameFactory gameFactory, ILevelService levelService, ILoadingScreenService loadingScreenService)
        {
            _sceneLoadService = sceneLoadService;
            _assetService = assetService;
            _screenService = screenService;
            _gameFactory = gameFactory;
            _levelService = levelService;
            _loadingScreenService = loadingScreenService;
        }

        void IEnterLoadState<string>.Enter(string sceneName)
        {
            _loadingScreenService.Show();
            
            _assetService.CleanUp();
            _screenService.CleanUp();
            _levelService.CleanUp();
            
            _sceneLoadService.Load(sceneName, CreateLevel);
        }

        void IExitState.Exit()
        {
            _loadingScreenService.Hide();
        }
        
        private void CreateLevel()
        {
            Level level = _gameFactory.CreateLevel();
            _gameFactory.CreatePlayer(level.PlayerSpawnPoint.position, Quaternion.identity, level.transform);
            
            _gameStateMachine.Enter<GameState>();
        }
    }
}