using UnityEngine;

namespace Core.Buildings
{
    [CreateAssetMenu(fileName = "BuildingConfig", menuName = "Game Configs/BuildingConfig", order = 0)]
    public class BuildingConfig : ScriptableObject
    {
        [SerializeField] private Vector2Int _size = Vector2Int.one;
        [SerializeField] private BuildingView _prefab;
        [SerializeField] private Sprite _icon;
        [SerializeField] private bool _isWalkable = false;
        
        public Vector2Int Size => _size;
        public BuildingView Prefab => _prefab;
        public Sprite Icon => _icon;
        public bool IsWalkable => _isWalkable;

        private void OnValidate()
        {
            if (_size.x < 1)
            {
                _size.x = 1;
            }
            if (_size.y < 1)
            {
                _size.y = 1;
            }
        }
    }
}