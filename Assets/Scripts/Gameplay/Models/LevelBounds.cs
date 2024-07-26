using Game.Infrastructure.CameraService;
using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;

namespace Game.Gameplay.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class LevelBounds : IInitializable
    {
        private readonly ICameraService _cameraService;

        public LevelBounds(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }
        
        public Vector2 ScreenBounds { get; private set; }
        public float BoundaryLine { get; private set; }

        void IInitializable.Initialize()
        {
            ScreenBounds = _cameraService.Camera
                .ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        }

        public void SetBoundaryLine(float boundaryLine) => BoundaryLine = boundaryLine;
    }
}