using System;
using Core.UI;
using Zenject;

namespace Core.Buildings
{
    public class AllBuildingsDestroy : IInitializable, IDisposable
    {
        private readonly BuildingContainer _buildingContainer;
        private readonly DestroyAllButton _destroyAllButton;

        public AllBuildingsDestroy(DestroyAllButton destroyAllButton, BuildingContainer buildingContainer)
        {
            _destroyAllButton = destroyAllButton;
            _buildingContainer = buildingContainer;
        }

        public void Dispose()
        {
            _destroyAllButton.ButtonClicked -= DestroyAllBuildings;
        }

        public void Initialize()
        {
            _destroyAllButton.ButtonClicked += DestroyAllBuildings;
        }

        private void DestroyAllBuildings()
        {
            _buildingContainer.DestroyAll();
        }
    }
}