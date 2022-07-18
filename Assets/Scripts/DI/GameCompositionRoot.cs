using Core.Buildings;
using Core.Camera;
using Core.Character;
using Core.Grid;
using Core.Grid.Cells;
using Core.Infrastructure;
using Core.Touch;
using Core.UI;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameCompositionRoot : MonoInstaller
    {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private StaticData _staticData;
        [SerializeField] private UISceneData _uiSceneData;
        
        public override void InstallBindings()
        {
            BindInfrastructure();
            BindGridRelated();
            BindUIRelated();
            BindBuildingsRelated();
            BindCharacterRelated();
            BindCameraRelated();
        }

        private void BindCameraRelated()
        {
            Container.BindSingle<CameraOffset>();
            Container.BindSingle<CameraOffsetControl>();
        }

        private void BindCharacterRelated()
        {
            Container.BindSingle<CharacterFactory>();
            Container.BindSingle<CharacterSpawn>();
            Container.BindSingle<CharacterContainer>();
        }

        private void BindBuildingsRelated()
        {
            Container.BindSingle<BuildingFactory>();
            Container.BindSingle<BuildingSpawn>();
            Container.BindSingle<BuildingContainer>();
            Container.BindSingle<ManualBuildingSpawn>();
            Container.BindSingle<DefaultBuildingSpawn>();
            Container.BindSingle<BuildingMove>();
            Container.BindSingle<AllBuildingsDestroy>();
        }

        private void BindGridRelated()
        {
            Container.BindSingle<CellViewPool>();
            Container.BindSingle<GridCellFactory>();
            Container.BindSingle<GridCellContainer>();
            Container.BindSingle<GridSpawner>();
            Container.BindSingle<GridNavigation>();
        }

        private void BindUIRelated()
        {
            Container.BindSingle<BuildButtons>();
            Container.BindSingle<DestroyAllButton>();
        }

        private void BindInfrastructure()
        {
            Container.BindInstance(_sceneData);
            Container.BindInstance(_staticData);
            Container.BindInstance(_uiSceneData);
            Container.BindSingle<LevelLoader>();
        }
    }
}