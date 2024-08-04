using System;
using Game.Infrastructure.AssetService;
using Game.Infrastructure.CameraService;
using Game.Infrastructure.Factories.GameFactory;
using Game.Infrastructure.Factories.ScreenFactory;
using Game.Infrastructure.Factories.StateMachineFactory;
using Game.Infrastructure.Factories.SystemFactory;
using Game.Infrastructure.GameStateMachine.States;
using Game.Infrastructure.InputService;
using Game.Infrastructure.JobService;
using Game.Infrastructure.LevelService;
using Game.Infrastructure.LoadingScreenService;
using Game.Infrastructure.ObjectPoolService;
using Game.Infrastructure.SceneLoadService;
using Game.Infrastructure.StaticDataService;
using Game.UI.ScreenService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scopes
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
            builder.Register<IJobService, JobService>(Lifetime.Singleton);
            builder.Register<IStateMachineFactory, StateMachineFactory>(Lifetime.Singleton);
            builder.Register<IScreenFactory, ScreenFactory>(Lifetime.Singleton);
            builder.Register<ISystemFactory, SystemFactory>(Lifetime.Singleton);
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
            
            builder.Register<IObjectPoolService, ObjectPoolService>(Lifetime.Singleton).WithParameter(transform).As<IDisposable>();

            builder.RegisterBuildCallback(CreateGameStateMachine);
        }
    }
}