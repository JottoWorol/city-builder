using UnityEngine;

namespace Core.Grid.Cells
{
    public struct GridCellData
    {
        public CellView CellView;
        public Vector2Int Position;
        
        public GridCellData(CellView cellView, Vector2Int position)
        {
            CellView = cellView;
            Position = position;
        }
    }
}