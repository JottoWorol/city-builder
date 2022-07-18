using Core.Grid;
using Core.Infrastructure;

namespace Core.Buildings
{
    public class BuildingSpawn
    {
        private readonly GridCellContainer _gridCellContainer;
        private readonly BuildingFactory _buildingFactory;
        private readonly GridView _gridView;
        
        public BuildingSpawn(GridCellContainer gridCellContainer, BuildingFactory buildingFactory, SceneData sceneData)
        {
            _gridCellContainer = gridCellContainer;
            _buildingFactory = buildingFactory;
            _gridView = sceneData.GridView;
        }

        public bool TrySpawnBuilding(BuildingConfig buildingConfig)
        {
            if(!_gridCellContainer.TryGetNearestFreeCellPositionsByShape(
                   buildingConfig.Size, 
                   _gridView.CenterPoint.position, out var freeCells))
            {
                return false;
            }

            if (!_buildingFactory.TrySpawnBuildingAtPositions(new BuildingData(buildingConfig, freeCells),
                    out var building
                ))
                return false;

            return true;
        }
    }
}