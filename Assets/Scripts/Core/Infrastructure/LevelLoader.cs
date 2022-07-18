using Core.Buildings;
using Core.Character;
using Core.Grid;
using Core.UI;
using Zenject;

namespace Core.Infrastructure
{
    public class LevelLoader : IInitializable
    {
        private readonly BuildButtons _buildButtons;
        private readonly CharacterSpawn _characterSpawn;
        private readonly DefaultBuildingSpawn _defaultBuildingSpawn;
        private readonly DestroyAllButton _destroyAllButton;
        private readonly GridSpawner _gridSpawner;

        public LevelLoader(GridSpawner gridSpawner, DefaultBuildingSpawn defaultBuildingSpawn,
            CharacterSpawn characterSpawn, DestroyAllButton destroyAllButton, BuildButtons buildButtons)
        {
            _gridSpawner = gridSpawner;
            _defaultBuildingSpawn = defaultBuildingSpawn;
            _characterSpawn = characterSpawn;
            _destroyAllButton = destroyAllButton;
            _buildButtons = buildButtons;
        }

        public void Initialize()
        {
            _gridSpawner.SpawnGrid();
            _characterSpawn.Initialize();
            _defaultBuildingSpawn.SpawnDefaultBuildings();

            _buildButtons.Initialize();
            _destroyAllButton.Initialize();
        }
    }
}