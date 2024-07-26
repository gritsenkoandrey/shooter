using UnityEngine;

namespace Game.Infrastructure.CameraService
{
    public interface ICameraService
    {
        Camera Camera { get; }
    }
}