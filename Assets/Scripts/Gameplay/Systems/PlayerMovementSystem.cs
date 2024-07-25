using Core.Implementation;
using Gameplay.Components;
using Infrastructure.CameraService;
using Infrastructure.InputService;
using Infrastructure.LevelService;
using UnityEngine;
using Utils;
using VContainer;

namespace Gameplay.Systems
{
    public sealed class PlayerMovementSystem : SystemComponent<Player>
    {
        private IInputService _inputService;
        private ICameraService _cameraService;
        private ILevelService _levelService;

        private Vector2 _screenBounds;

        [Inject]
        private void Construct(IInputService inputService, ICameraService cameraService, ILevelService levelService)
        {
            _inputService = inputService;
            _cameraService = cameraService;
            _levelService = levelService;
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _screenBounds = _cameraService.Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Move);
        }

        private void Move(Player player)
        {
            Vector3 input = new Vector3(_inputService.Horizontal, _inputService.Vertical, 0f);
            Vector3 position = player.Position + input * player.Speed * Time.deltaTime;
            float minX = -_screenBounds.x + player.CollisionRadius;
            float minY = -_screenBounds.y + player.CollisionRadius;
            float maxX = _screenBounds.x - player.CollisionRadius;
            float maxY = _levelService.Finish - player.CollisionRadius;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);
            player.transform.position = position;
        }
    }
}