using Core.Buildings;
using Core.Camera;
using Core.Character;
using Core.Grid;
using Core.Grid.Cells;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Infrastructure
{
    public class StaticData : MonoBehaviour
    {
        [SerializeField] private CellView _cellViewPrefab;
        [SerializeField] private GridConfig _gridConfig;
        [SerializeField] private BuildingList _buildingList;
        [SerializeField] private BuildButtonView _buildViewButtonPrefab;
        [SerializeField] private InitialGridStateConfig _initialGridStateConfig;
        [SerializeField] private CharacterConfig _defaultCharacterConfig;
        [SerializeField] private Button _destroyAllButtonPrefab;
        [SerializeField] private CameraControlConfig _cameraControlConfig;

        public CellView CellViewPrefab => _cellViewPrefab;
        public GridConfig GridConfig => _gridConfig;
        public BuildingList BuildingList => _buildingList;
        public BuildButtonView BuildViewButtonPrefab => _buildViewButtonPrefab;
        public InitialGridStateConfig InitialGridStateConfig => _initialGridStateConfig;
        public CharacterConfig DefaultCharacterConfig => _defaultCharacterConfig;
        public Button DestroyAllButtonPrefab => _destroyAllButtonPrefab;
        public CameraControlConfig CameraControlConfig => _cameraControlConfig;
    }
}