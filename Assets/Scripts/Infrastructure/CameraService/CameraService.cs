using UnityEngine;

namespace Infrastructure.CameraService
{
    public sealed class CameraService : MonoBehaviour, ICameraService
    {
        [SerializeField] private Camera _camera;

        Camera ICameraService.Camera => _camera;
    }
}