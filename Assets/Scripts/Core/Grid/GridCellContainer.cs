using System;
using System.Collections.Generic;
using Core.Buildings;
using Core.Grid.Cells;
using Core.Infrastructure;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Grid
{
    public class GridCellContainer
    {
        private readonly List<GridCell> _cells = new List<GridCell>();
        private readonly GridConfig _gridConfig;

        private readonly Dictionary<Vector2Int, GridCell> _positionToCell = new Dictionary<Vector2Int, GridCell>();
        private readonly Dictionary<CellView, GridCell> _viewToCell = new Dictionary<CellView, GridCell>();

        public GridCellContainer(StaticData staticData) => _gridConfig = staticData.GridConfig;

        public void AddCell(GridCell cell)
        {
            _cells.Add(cell);
            _positionToCell.Add(cell.GridCellData.Position, cell);
            _viewToCell.Add(cell.GridCellData.CellView, cell);
        }

        public void RemoveCell(GridCell cell)
        {
            _cells.Remove(cell);
            _positionToCell.Remove(cell.GridCellData.Position);
            _viewToCell.Remove(cell.GridCellData.CellView);
        }

        public GridCell GetCellByPosition(Vector2Int position)
        {
            if (_positionToCell.TryGetValue(position, out var cell))
                return cell;

            throw new Exception("Cell with position " + position + " not found");
        }

        public bool TryGetRandomWalkablePosition(out Vector2Int position)
        {
            var walkableCells = new List<GridCell>();

            foreach (var cell in _cells)
            {
                if (cell.IsWalkable)
                    walkableCells.Add(cell);
            }

            if (walkableCells.Count == 0)
            {
                position = Vector2Int.zero;
                return false;
            }

            var randomIndex = Random.Range(0, walkableCells.Count);
            position = walkableCells[randomIndex].GridCellData.Position;
            return true;
        }

        public bool TryGetCellByView(CellView view, out GridCell cell) => _viewToCell.TryGetValue(view, out cell);

        public bool IsWalkable(Vector2Int position) => GetCellByPosition(position).IsWalkable;

        public Vector2Int[] GetNearestCellPositions(Vector2Int shape, Vector3 point)
        {
            var sortedShapeOrigins = GetShapeOriginsSortedByDistance(shape, point);
            var result = new Vector2Int[shape.x * shape.y];

            for (var x = 0; x < shape.x; x++)
            {
                for (var y = 0; y < shape.y; y++)
                {
                    result[x + y * shape.x] = sortedShapeOrigins[0].Key + new Vector2Int(x, y);
                }
            }

            return result;
        }

        public bool TryGetNearestFreeCellPositionsByShape(Vector2Int shape, Vector3 point, out Vector2Int[] cells)
        {
            var sortedShapeOrigins = GetShapeOriginsSortedByDistance(shape, point);
            var result = new Vector2Int[shape.x * shape.y];

            foreach (var shapeOrigin in sortedShapeOrigins)
            {
                var hasNonFreeCell = false;
                for (var x = 0; x < shape.x; x++)
                {
                    for (var y = 0; y < shape.y; y++)
                    {
                        var position = shapeOrigin.Key + new Vector2Int(x, y);
                        if (GetCellByPosition(position).ChildBuilding != null)
                        {
                            hasNonFreeCell = true;
                            break;
                        }

                        result[x + y * shape.x] = position;
                    }
                }

                if (!hasNonFreeCell)
                {
                    cells = result;
                    return true;
                }
            }

            cells = null;
            return false;
        }

        public bool IsFreeAtPosition(Vector2Int gridPosition) => GetCellByPosition(gridPosition).ChildBuilding == null;

        public bool IsOccupiedBy(Vector2Int gridPosition, Building building) =>
            GetCellByPosition(gridPosition).ChildBuilding.Equals(building);

        public Vector3 GetAverageWorldPosition(Vector2Int[] gridPositions)
        {
            var averagePosition = Vector3.zero;

            foreach (var gridPosition in gridPositions)
            {
                averagePosition += GetCellByPosition(gridPosition).WorldPosition;
            }

            return averagePosition / gridPositions.Length;
        }

        public int GetSortingOrderForPosition(Vector2Int gridPosition) => -gridPosition.y * gridPosition.x;

        public void OccupyCell(Vector2Int gridPosition, Building building)
        {
            var cell = GetCellByPosition(gridPosition);
            cell.SetChildBuilding(building);
        }

        public void FreeCell(Vector2Int gridPosition)
        {
            var cell = GetCellByPosition(gridPosition);
            cell.RemoveChildBuilding();
        }

        private KeyValuePair<Vector2Int, float>[] GetShapeOriginsSortedByDistance(Vector2Int shape, Vector3 point)
        {
            var gridDelta = new Vector2Int[shape.x * shape.y];
            for (var x = 0; x < shape.x; x++)
            {
                for (var y = 0; y < shape.y; y++)
                {
                    gridDelta[x + y * shape.x] = new Vector2Int(x, y);
                }
            }

            var boundX = _gridConfig.MapSize.x - shape.x;
            var boundY = _gridConfig.MapSize.y - shape.y;
            var dictionary = new KeyValuePair<Vector2Int, float>[boundX * boundY];
            var tempGridPositions = new Vector2Int[shape.x * shape.y];

            for (var x = 0; x < boundX; x++)
            {
                for (var y = 0; y < boundY; y++)
                {
                    var gridPoint = new Vector2Int(x, y);

                    for (var i = 0; i < shape.x * shape.y; i++)
                    {
                        tempGridPositions[i] = gridDelta[i] + gridPoint;
                    }

                    var distance = Vector3.Distance(GetAverageWorldPosition(tempGridPositions), point);
                    dictionary[x + y * boundX] = new KeyValuePair<Vector2Int, float>(new Vector2Int(x, y), distance);
                }
            }

            Array.Sort(dictionary, (a, b) => a.Value.CompareTo(b.Value));
            return dictionary;
        }
    }
}