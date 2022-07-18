using System.Collections.Generic;
using Core.Infrastructure;
using UnityEngine;

namespace Core.Grid
{
    public class GridNavigation
    {
        private readonly GridCellContainer _gridCellContainer;
        private readonly GridConfig _gridConfig;

        private readonly List<Vector2Int> _visitedCells = new List<Vector2Int>();
        private List<Vector2Int> _foundPath;

        public GridNavigation(StaticData staticData, GridCellContainer gridCellContainer)
        {
            _gridConfig = staticData.GridConfig;
            _gridCellContainer = gridCellContainer;
        }

        public List<Vector2Int> GetPath(Vector2Int start, Vector2Int end)
        {
            _visitedCells.Clear();
            _foundPath = null;
            TryGoToCell(new List<Vector2Int>(), start, end);
            return _foundPath;
        }

        private bool TryGoToCell(List<Vector2Int> path, Vector2Int nextPosition, Vector2Int targetPosition)
        {
            if (path.Count > _gridConfig.MaxPathLength)
                return false;

            if (nextPosition.y < 0 || nextPosition.y >= _gridConfig.MapSize.y
                                   || nextPosition.x < 0 || nextPosition.x >= _gridConfig.MapSize.x)
                return false;

            if (_visitedCells.Contains(nextPosition) || !_gridCellContainer.IsWalkable(nextPosition) ||
                _foundPath != null)
                return false;

            _visitedCells.Add(nextPosition);
            path.Add(nextPosition);

            if (nextPosition == targetPosition)
            {
                _foundPath = path;
                return true;
            }

            return TryGoToCell(new List<Vector2Int>(path), nextPosition + Vector2Int.up, targetPosition) ||
                   TryGoToCell(new List<Vector2Int>(path), nextPosition + Vector2Int.right, targetPosition) ||
                   TryGoToCell(new List<Vector2Int>(path), nextPosition + Vector2Int.down, targetPosition) ||
                   TryGoToCell(new List<Vector2Int>(path), nextPosition + Vector2Int.left, targetPosition);
        }
    }
}