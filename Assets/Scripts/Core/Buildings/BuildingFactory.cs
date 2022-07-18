using System;
using Core.Grid;
using Core.Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Buildings
{
    public class BuildingFactory
    {
        private readonly BuildingContainer _buildingContainer;
        private readonly Transform _buildingParent;
        private readonly GridCellContainer _gridCellContainer;
        private readonly GridConfig _gridConfig;
        private readonly Transform _walkableParent;

        public BuildingFactory(GridCellContainer gridCellContainer, BuildingContainer buildingContainer,
            SceneData sceneData, StaticData staticData)
        {
            _gridCellContainer = gridCellContainer;
            _buildingContainer = buildingContainer;
            _buildingParent = sceneData.BuildingParent;
            _walkableParent = sceneData.WalkableParent;
            _gridConfig = staticData.GridConfig;
        }

        public event Action<Building> BuildingSpawned;

        public bool TrySpawnBuildingAtPositions(BuildingData buildingData, out Building building)
        {
            var positions = buildingData.OccupiedGridPosition;
            var config = buildingData.Building;

            foreach (var gridPosition in positions)
            {
                if (!_gridCellContainer.IsFreeAtPosition(gridPosition))
                {
                    building = null;
                    return false;
                }
            }

            BuildingView view;

            if (config.IsWalkable)
                view = Object.Instantiate(config.Prefab, _walkableParent);
            else
                view = Object.Instantiate(config.Prefab, _buildingParent);

            var touchColliderSize = view.TouchCollider.size;
            touchColliderSize.x = _gridConfig.CellSize * config.Size.x;
            touchColliderSize.z = _gridConfig.CellSize * config.Size.y;
            view.TouchCollider.size = touchColliderSize;

            var newBuilding = new Building(view, config);

            _buildingContainer.Add(newBuilding);
            _buildingContainer.UpdateOccupiedCellsTo(newBuilding, positions);
            _buildingContainer.UpdatePosition(newBuilding);
            building = newBuilding;
            BuildingSpawned?.Invoke(newBuilding);
            return true;
        }
    }
}