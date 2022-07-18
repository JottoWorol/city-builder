using System;
using Core.Grid;
using Core.Infrastructure;
using Core.Touch;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Buildings
{
    public class BuildingMove : IInitializable, IDisposable
    {
        private readonly BuildingContainer _buildingContainer;
        private readonly UnityEngine.Camera _camera;
        private readonly GridCellContainer _gridCellContainer;
        private readonly GridConfig _gridConfig;
        private readonly ScreenTouchView _screenTouchView;

        private Vector2Int[] _currentPositionCells;
        private Vector3 _initialBuildingPosition;
        private Vector3 _initialTouchPosition;
        private Building _targetBuilding;
        private Vector3 _totalDelta;

        public BuildingMove(UISceneData uiSceneData, SceneData sceneData, StaticData staticData,
            GridCellContainer gridCellContainer, BuildingContainer buildingContainer)
        {
            _gridCellContainer = gridCellContainer;
            _buildingContainer = buildingContainer;
            _screenTouchView = uiSceneData.ScreenTouchView;
            _camera = sceneData.Camera;
            _gridConfig = staticData.GridConfig;
        }

        public bool IsMoving { get; private set; }

        public void Dispose()
        {
            _screenTouchView.PointerDown -= OnPointerDown;
            _screenTouchView.PointerUp -= OnPointerUp;
            _screenTouchView.PointerDrag -= OnPointerDrag;
        }

        public void Initialize()
        {
            _screenTouchView.PointerDown += OnPointerDown;
            _screenTouchView.PointerUp += OnPointerUp;
            _screenTouchView.PointerDrag += OnPointerDrag;
        }

        private void StartMoveSession(Building building)
        {
            IsMoving = true;
            _totalDelta = Vector3.zero;
            _targetBuilding = building;
            _initialBuildingPosition = building.BuildingView.transform.position;
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            if (IsMoving
                || !TryGetBuildingByScreenPosition(eventData.position, out var building)
                || !TryGetTouchPoint(eventData.position, out _initialTouchPosition))
                return;

            StartMoveSession(building);
        }

        private void OnPointerUp(PointerEventData obj)
        {
            if (!IsMoving)
                return;

            IsMoving = false;
            var currentCellsFree = true;

            foreach (var position in _currentPositionCells)
            {
                if (!_gridCellContainer.IsFreeAtPosition(position) &&
                    !_gridCellContainer.IsOccupiedBy(position, _targetBuilding))
                {
                    currentCellsFree = false;
                    break;
                }
            }

            if (currentCellsFree)
            {
                _buildingContainer.UpdateOccupiedCellsTo(_targetBuilding, _currentPositionCells);
                _buildingContainer.UpdatePosition(_targetBuilding);
            }
            else
            {
                if (!_gridCellContainer.TryGetNearestFreeCellPositionsByShape(
                        _targetBuilding.BuildingConfig.Size,
                        _targetBuilding.BuildingView.transform.position, out var newCells
                    ))
                    throw new Exception("Could not find free cells");

                _buildingContainer.UpdateOccupiedCellsTo(_targetBuilding, newCells);
                _buildingContainer.UpdatePosition(_targetBuilding);
            }

            _targetBuilding = null;
        }

        private void OnPointerDrag(PointerEventData eventData)
        {
            if (!IsMoving)
                return;

            if (TryGetTouchPoint(eventData.position, out var currentTouchPosition))
                _totalDelta = currentTouchPosition - _initialTouchPosition;

            _currentPositionCells = _gridCellContainer.GetNearestCellPositions(_targetBuilding.BuildingConfig.Size,
                _initialBuildingPosition + _totalDelta
            );

            _targetBuilding.BuildingView.transform.position =
                _gridCellContainer.GetAverageWorldPosition(_currentPositionCells);
        }

        private bool TryGetBuildingByScreenPosition(Vector2 screenPosition, out Building building)
        {
            var ray = _camera.ScreenPointToRay(screenPosition);
            var hit = Physics.Raycast(ray, out var hitInfo, _gridConfig.BuildingLayer);

            if (hit && hitInfo.collider.TryGetComponent(out BuildingView view))
                return _buildingContainer.TryGetBuildingByView(view, out building);

            building = null;
            return false;
        }

        private bool TryGetTouchPoint(Vector2 screenPosition, out Vector3 touchPoint)
        {
            var ray = _camera.ScreenPointToRay(screenPosition);
            var hit = Physics.Raycast(ray, out var hitInfo, _gridConfig.GridLayer);

            if (hit)
            {
                touchPoint = hitInfo.point;
                return true;
            }

            touchPoint = Vector3.zero;
            return false;
        }
    }
}