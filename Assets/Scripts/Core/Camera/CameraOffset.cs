using Core.Infrastructure;
using UnityEngine;

namespace Core.Camera
{
    public class CameraOffset
    {
        private readonly Transform _cameraTransform;

        public CameraOffset(SceneData sceneData) => _cameraTransform = sceneData.Camera.transform;

        public Vector3 Offset
        {
            get => _cameraTransform.localPosition;
            set => _cameraTransform.localPosition = value;
        }
    }
}