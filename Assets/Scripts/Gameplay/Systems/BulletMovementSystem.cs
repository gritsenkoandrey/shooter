using Core.Implementation;
using Gameplay.Components;
using Infrastructure.CameraService;
using UnityEngine;
using Utils;
using VContainer;

namespace Gameplay.Systems
{
    public sealed class BulletMovementSystem : SystemComponent<Bullet>
    {
        private ICameraService _cameraService;
        
        private Vector2 _screenBounds;

        [Inject]
        private void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
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

        private void Move(Bullet bullet)
        {
            bullet.transform.Translate(bullet.Direction * bullet.Speed * Time.deltaTime);

            if (_screenBounds.x < bullet.Position.x || _screenBounds.y < bullet.Position.y)
            {
                Object.Destroy(bullet.gameObject);
            }
        }
    }
}