using System;
using Core.Buildings;
using Core.Grid;

namespace Core.Character
{
    public class CharacterSpawn : IDisposable
    {
        private readonly BuildingFactory _buildingFactory;
        private readonly CharacterFactory _characterFactory;

        private readonly GridCellContainer _gridCellContainer;

        public CharacterSpawn(BuildingFactory buildingFactory, CharacterFactory characterFactory,
            GridCellContainer gridCellContainer)
        {
            _buildingFactory = buildingFactory;
            _characterFactory = characterFactory;
            _gridCellContainer = gridCellContainer;
        }

        public void Dispose()
        {
            _buildingFactory.BuildingSpawned -= OnBuildingSpawned;
        }

        public void Initialize()
        {
            _buildingFactory.BuildingSpawned += OnBuildingSpawned;
        }

        private void OnBuildingSpawned(Building building)
        {
            if (building.BuildingConfig.IsWalkable ||
                !_gridCellContainer.TryGetRandomWalkablePosition(out var position))
                return;

            building.AttachCharacter(_characterFactory.SpawnCharacterAt(position));
        }
    }
}