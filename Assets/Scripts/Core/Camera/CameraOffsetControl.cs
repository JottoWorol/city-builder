using System;
using Core.Buildings;
using Core.Infrastructure;
using Core.Touch;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Camera
{
    public class CameraOffsetControl : IInitializable, IDisposable
    {
        private readonly BuildingMove _buildingMove;
        private readonly CameraControlConfig _cameraControlConfig;
        private readonly CameraOffset _cameraOffset;
        private readonly ScreenTouchView _screenTouchView;

        public CameraOffsetControl(UISceneData uiSceneData, BuildingMove buildingMove, CameraOffset cameraOffset,
            StaticData staticData)
        {
            _buildingMove = buildingMove;
            _cameraOffset = cameraOffset;
            _screenTouchView = uiSceneData.ScreenTouchView;
            _cameraControlConfig = staticData.CameraControlConfig;
        }

        public void Dispose()
        {
            _screenTouchView.PointerDrag -= OnPointerDrag;
        }

        public void Initialize()
        {
            _screenTouchView.PointerDrag += OnPointerDrag;
        }

        private void OnPointerDrag(PointerEventData pointerEventData)
        {
            if (_buildingMove.IsMoving)
                return;

            _cameraOffset.Offset -= new Vector3(pointerEventData.delta.x, pointerEventData.delta.y, 0) *
                                    _cameraControlConfig.CameraMoveSensitivity;
        }
    }
}