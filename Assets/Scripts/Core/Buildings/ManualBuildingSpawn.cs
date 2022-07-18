using System;
using Core.UI;
using Zenject;

namespace Core.Buildings
{
    public class ManualBuildingSpawn : IInitializable, IDisposable
    {
        private readonly BuildingSpawn _buildingSpawn;
        private readonly BuildButtons _buildButtons;
        
        public ManualBuildingSpawn(BuildingSpawn buildingSpawn, BuildButtons buildButtons)
        {
            _buildingSpawn = buildingSpawn;
            _buildButtons = buildButtons;
        }

        public void Initialize()
        {
            _buildButtons.ClickedForBuild += OnClickedForBuild;
        }

        public void Dispose()
        {
            _buildButtons.ClickedForBuild -= OnClickedForBuild;
        }

        private void OnClickedForBuild(BuildingConfig building)
        {
            _buildingSpawn.TrySpawnBuilding(building);
        }
    }
}