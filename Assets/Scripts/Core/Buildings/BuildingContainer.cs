using System.Collections.Generic;
using Core.Character;
using Core.Grid;
using UnityEngine;

namespace Core.Buildings
{
    public class BuildingContainer
    {
        private readonly Dictionary<BuildingView, Building> _buildingViewToBuilding =
            new Dictionary<BuildingView, Building>();

        private readonly GridCellContainer _gridCellContainer;
        private readonly CharacterContainer _characterContainer;
        private readonly List<Building> _buildings = new List<Building>();

        public BuildingContainer(GridCellContainer gridCellContainer, CharacterContainer characterContainer)
        {
            _gridCellContainer = gridCellContainer;
            _characterContainer = characterContainer;
        }

        public void Add(Building building)
        {
            _buildings.Add(building);
            _buildingViewToBuilding.Add(building.BuildingView, building);
        }

        public void Destroy(Building building)
        {
            building.Destroy();
            _buildings.Remove(building);
            _buildingViewToBuilding.Remove(building.BuildingView);

            foreach (var character in building.AttachedCharacters)
            {
                _characterContainer.DestroyCharacter(character);
            }

            if (building.OccupiedCellPositions != null)
                foreach (var position in building.OccupiedCellPositions)
                {
                    _gridCellContainer.FreeCell(position);
                }
        }

        public bool TryGetBuildingByView(BuildingView buildingView, out Building building) =>
            _buildingViewToBuilding.TryGetValue(buildingView, out building);

        public void UpdateOccupiedCellsTo(Building building, Vector2Int[] newCells)
        {
            if (building.OccupiedCellPositions != null)
                foreach (var position in building.OccupiedCellPositions)
                {
                    _gridCellContainer.FreeCell(position);
                }

            building.SetOccupiedCellPositions(newCells);
            foreach (var cell in newCells)
            {
                _gridCellContainer.OccupyCell(cell, building);
            }
        }

        public void UpdatePosition(Building building)
        {
            building.BuildingView.transform.position =
                _gridCellContainer.GetAverageWorldPosition(building.OccupiedCellPositions);

            building.BuildingView.SpriteRenderer.sortingOrder =
                _gridCellContainer.GetSortingOrderForPosition(building.OccupiedCellPositions[0]);
        }

        public void DestroyAll()
        {
            while (_buildings.Count > 0)
            {
                Destroy(_buildings[0]);
            }
        }
    }
}