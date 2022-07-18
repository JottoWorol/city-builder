using Core.Buildings;
using UnityEngine;

namespace Core.Grid.Cells
{
    public class GridCell
    {
        public GridCell(GridCellData gridCellData) => GridCellData = gridCellData;

        public GridCellData GridCellData { get; }
        public Building ChildBuilding { get; private set; }
        public Vector3 WorldPosition => GridCellData.CellView.Transform.position;
        public bool IsWalkable { get; private set; }

        public void SetChildBuilding(Building building)
        {
            ChildBuilding = building;
            IsWalkable = building.BuildingConfig.IsWalkable;
        }

        public void RemoveChildBuilding()
        {
            ChildBuilding = null;
            IsWalkable = false;
        }
    }
}