using Core.Grid;
using Core.Infrastructure;

namespace Core.Buildings
{
    public class DefaultBuildingSpawn
    {
        private readonly BuildingFactory _buildingFactory;
        private readonly InitialGridStateConfig _initialGridStateConfig;

        public DefaultBuildingSpawn(StaticData staticData, BuildingFactory buildingFactory)
        {
            _buildingFactory = buildingFactory;
            _initialGridStateConfig = staticData.InitialGridStateConfig;
        }

        public void SpawnDefaultBuildings()
        {
            foreach (var buildingData in _initialGridStateConfig.BuildingsData)
            {
                _buildingFactory.TrySpawnBuildingAtPositions(buildingData, out var building);
            }
        }
    }
}