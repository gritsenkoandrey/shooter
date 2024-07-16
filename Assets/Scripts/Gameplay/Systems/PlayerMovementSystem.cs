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
            Vector3 move = new Vector3(_inputService.Horizontal, _inputService.Vertical, 0f);

            player.transform.Translate(move * player.Speed * Time.deltaTime);

            Vector3 next = player.Position;

            next.x = Mathf.Clamp(next.x, -_screenBounds.x + player.CollisionRadius, _screenBounds.x - player.CollisionRadius);
            next.y = Mathf.Clamp(next.y, -_screenBounds.y + player.CollisionRadius, _levelService.Finish - player.CollisionRadius);

            player.transform.position = next;
        }
    }
}