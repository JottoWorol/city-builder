using Core.Grid.Cells;
using UnityEngine;

namespace Core.Grid
{
    public class GridCellFactory
    {
        private readonly CellViewPool _cellViewPool;

        public GridCellFactory(CellViewPool cellViewPool) => _cellViewPool = cellViewPool;

        public GridCell GetGridCellWithPosition(Vector2Int position) =>
            new GridCell(new GridCellData(_cellViewPool.GetCellView(), position));
    }
}