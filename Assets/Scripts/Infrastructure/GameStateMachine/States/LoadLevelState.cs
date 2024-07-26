using Game.Gameplay.Entities;
using Game.Infrastructure.AssetService;
using Game.Infrastructure.Factories.GameFactory;
using Game.Infrastructure.LevelService;
using Game.Infrastructure.LoadingScreenService;
using Game.Infrastructure.ObjectPoolService;
using Game.Infrastructure.SceneLoadService;
using Game.UI.ScreenService;
using UnityEngine;
using VContainer;

namespace Game.Infrastructure.GameStateMachine.States
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
        private IObjectPoolService _objectPoolService;

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