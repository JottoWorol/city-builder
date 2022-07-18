using System;
using Core.Infrastructure;
using UnityEngine;

namespace Core.Grid
{
    public class GridSpawner
    {
        private const int PositionPrecision = 1;
        private readonly GridCellContainer _cellContainer;
        private readonly GridCellFactory _cellFactory;
        private readonly GridConfig _gridConfig;

        private readonly Vector3 _originPoint;

        public GridSpawner(SceneData sceneData, StaticData staticData, GridCellFactory cellFactory,
            GridCellContainer cellContainer)
        {
            _cellFactory = cellFactory;
            _cellContainer = cellContainer;
            var gridView = sceneData.GridView;
            _gridConfig = staticData.GridConfig;

            _originPoint = gridView.CenterPoint.position
                           - new Vector3((_gridConfig.MapSize.x - 1f) / 2, 0, (_gridConfig.MapSize.y - 1f) / 2)
                           * _gridConfig.CellSize;
        }

        public void SpawnGrid()
        {
            for (var x = 0; x < _gridConfig.MapSize.x; x++)
            {
                for (var y = 0; y < _gridConfig.MapSize.y; y++)
                {
                    var gridPosition = new Vector2Int(x, y);
                    var cell = _cellFactory.GetGridCellWithPosition(gridPosition);
                    cell.GridCellData.CellView.Transform.position = GetPosition(gridPosition);
                    _cellContainer.AddCell(cell);
                }
            }
        }

        private Vector3 GetPosition(Vector2Int gridPosition)
        {
            var position = _originPoint + new Vector3(gridPosition.x, 0, gridPosition.y) * _gridConfig.CellSize;
            return new Vector3(Round(position.x, PositionPrecision), 0, Round(position.z, PositionPrecision));
        }

        private float Round(float value, int digits) => (float) Math.Round(value, digits);
    }
}