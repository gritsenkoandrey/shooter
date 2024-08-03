using System.Runtime.CompilerServices;
using Game.Core.Implementation;
using Game.Gameplay.Entities;
using Game.Gameplay.Models;
using Game.Infrastructure.InputService;
using Game.Utils;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Systems
{
    public sealed class PlayerMovementSystem : SystemComponent<Player>
    {
        private IInputService _inputService;
        private LevelBounds _levelBounds;

        [Inject]
        private void Construct(IInputService inputService, LevelBounds levelBounds)
        {
            _inputService = inputService;
            _levelBounds = levelBounds;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Execute);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Execute(Player component)
        {
            Vector3 input = new Vector3(_inputService.Horizontal, _inputService.Vertical, 0f);
            Vector3 position = component.Position + input * component.Move.Speed * Time.deltaTime;
            float minX = -_levelBounds.ScreenBounds.x + component.Radius;
            float minY = -_levelBounds.ScreenBounds.y + component.Radius;
            float maxX = _levelBounds.ScreenBounds.x - component.Radius;
            float maxY = _levelBounds.BoundaryLine - component.Radius;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);
            component.transform.position = position;
        }
    }
}