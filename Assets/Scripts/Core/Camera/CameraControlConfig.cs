using UnityEngine;

namespace Core.Camera
{
    [CreateAssetMenu(fileName = "CameraControlConfig", menuName = "Game Configs/CameraControlConfig",
        order = 0
    )]
    public class CameraControlConfig : ScriptableObject
    {
        [SerializeField] private float _cameraMoveSensitivity = 10f;
        
        public float CameraMoveSensitivity => _cameraMoveSensitivity;
    }
}