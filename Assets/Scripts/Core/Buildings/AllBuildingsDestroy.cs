using System;
using Core.UI;
using Zenject;

namespace Core.Buildings
{
    public class AllBuildingsDestroy : IInitializable, IDisposable
    {
        private readonly DestroyAllButton _destroyAllButton;
        private readonly BuildingContainer _buildingContainer;
        
        public AllBuildingsDestroy(DestroyAllButton destroyAllButton, BuildingContainer buildingContainer)
        {
            _destroyAllButton = destroyAllButton;
            _buildingContainer = buildingContainer;
        }

        public void Initialize()
        {
            _destroyAllButton.ButtonClicked += DestroyAllBuildings;
        }

        public void Dispose()
        {
            _destroyAllButton.ButtonClicked -= DestroyAllBuildings;
        }

        private void DestroyAllBuildings()
        {
            _buildingContainer.DestroyAll();
        }
    }
}