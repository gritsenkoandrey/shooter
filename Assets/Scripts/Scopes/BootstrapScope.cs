using Infrastructure.AssetService;
using Infrastructure.CameraService;
using Infrastructure.Factories.GameFactory;
using Infrastructure.Factories.ScreenFactory;
using Infrastructure.Factories.StateMachineFactory;
using Infrastructure.Factories.SystemFactory;
using Infrastructure.GameStateMachine.States;
using Infrastructure.InputService;
using Infrastructure.LevelService;
using Infrastructure.LoadingScreenService;
using Infrastructure.SceneLoadService;
using Infrastructure.StaticDataService;
using UI.ScreenService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public sealed class BootstrapScope : LifetimeScope
    {
        [SerializeField] private ScreenService _screenService;
        [SerializeField] private CameraService _cameraService;
        [SerializeField] private LoadingScreenService _loadingScreenService;

        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(this);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            void CreateGameStateMachine(IObjectResolver container) => 
                container.Resolve<IStateMachineFactory>().CreateGameStateMachine().Enter<BootstrapState>();

            builder.RegisterComponentInNewPrefab(_screenService, Lifetime.Singleton).UnderTransform(transform).As<IScreenService>();
            builder.RegisterComponentInNewPrefab(_cameraService, Lifetime.Singleton).UnderTransform(transform).As<ICameraService>();
            builder.RegisterComponentInNewPrefab(_loadingScreenService, Lifetime.Singleton).UnderTransform(transform).As<ILoadingScreenService>();

            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            builder.Register<ISceneLoadService, SceneLoadService>(Lifetime.Singleton);
            builder.Register<IAssetService, AssetService>(Lifetime.Singleton);
            builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
            builder.Register<ILevelService, LevelService>(Lifetime.Singleton);
            builder.Register<IStateMachineFactory, StateMachineFactory>(Lifetime.Singleton);
            builder.Register<IScreenFactory, ScreenFactory>(Lifetime.Singleton);
            builder.Register<ISystemFactory, SystemFactory>(Lifetime.Singleton);
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);

            builder.RegisterBuildCallback(CreateGameStateMachine);
        }
    }
}