using UnityEngine;

namespace Infrastructure.CameraService
{
    public interface ICameraService
    {
        Camera Camera { get; }
    }
}