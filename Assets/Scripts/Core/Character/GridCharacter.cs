using System;
using System.Collections.Generic;
using Core.Grid;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Character
{
    public class GridCharacter
    {
        private readonly CharacterConfig _characterConfig;
        private readonly CharacterView _characterView;
        private readonly GridCellContainer _gridCellContainer;
        private readonly GridNavigation _gridNavigation;

        private List<Vector2Int> _currentPath = new List<Vector2Int>();
        private int _currentPathIndex;
        private bool _isMoving;
        private Vector3 _targetWorldPosition;

        public GridCharacter(CharacterView characterView, GridNavigation gridNavigation,
            GridCellContainer gridCellContainer, CharacterConfig characterConfig, Vector2Int initialPosition)
        {
            _characterView = characterView;
            _gridNavigation = gridNavigation;
            _gridCellContainer = gridCellContainer;
            _characterConfig = characterConfig;
            CurrentGridPosition = initialPosition;

            MoveToNextPoint();
        }

        public Vector2Int CurrentGridPosition { get; private set; }
        public Vector2Int CurrentDirection { get; private set; }
        public event Action DirectionChanged;
        private void SetSortingOrder(int order) => _characterView.SpriteRenderer.sortingOrder = order;
        public void SetSpriteMirror(bool mirror) => _characterView.SpriteRenderer.flipX = mirror;

        private void MoveToNextPoint()
        {
            if (_currentPathIndex >= _currentPath.Count)
            {
                _currentPath = null;
                do
                {
                    if (!_gridCellContainer.TryGetRandomWalkablePosition(out var nextPosition))
                        return;

                    _currentPath = _gridNavigation.GetPath(CurrentGridPosition, nextPosition);
                } while (_currentPath == null);

                _currentPathIndex = 0;
            }

            MoveComplete += OnMoveComplete;
            MoveTo(_currentPath[_currentPathIndex]);
        }

        private void OnMoveComplete()
        {
            MoveComplete -= OnMoveComplete;
            _currentPathIndex++;
            MoveToNextPoint();
        }

        private event Action MoveComplete;

        private void MoveTo(Vector2Int gridPosition)
        {
            if (CurrentGridPosition == gridPosition)
            {
                MoveComplete?.Invoke();
                return;
            }

            CurrentDirection = gridPosition - CurrentGridPosition;
            DirectionChanged?.Invoke();
            var targetCell = _gridCellContainer.GetCellByPosition(gridPosition);
            _targetWorldPosition = targetCell.WorldPosition;
            CurrentGridPosition = gridPosition;
            SetSortingOrder(_gridCellContainer.GetSortingOrderForPosition(CurrentGridPosition) + 1);

            _isMoving = true;
        }

        public void Update()
        {
            if (!_isMoving)
                return;

            _characterView.transform.position =
                Vector3.MoveTowards(
                    _characterView.transform.position,
                    _targetWorldPosition,
                    _characterConfig.MoveSpeed * Time.deltaTime
                );

            if (_characterView.transform.position == _targetWorldPosition)
            {
                _isMoving = false;
                MoveComplete?.Invoke();
            }
        }

        public event Action Destroyed;

        public void Destroy()
        {
            Object.Destroy(_characterView.gameObject);
            Destroyed?.Invoke();
        }
    }
}