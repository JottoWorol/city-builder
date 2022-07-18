using Core.Grid;
using UnityEngine;

namespace Core.Infrastructure
{
    public class SceneData : MonoBehaviour
    {
        [SerializeField] private Transform _cellViewPoolParent;
        [SerializeField] private GridView _gridView;
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform _buildingParent;
        [SerializeField] private Transform _characterParent;
        [SerializeField] private Transform _walkableParent;

        public Transform CellViewPoolParent => _cellViewPoolParent;
        public GridView GridView => _gridView;
        public UnityEngine.Camera Camera => _camera;
        public Transform BuildingParent => _buildingParent;
        public Transform CharacterParent => _characterParent;
        public Transform WalkableParent => _walkableParent;
    }
}