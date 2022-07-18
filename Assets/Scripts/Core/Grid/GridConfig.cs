using UnityEngine;

namespace Core.Grid
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Game Configs/GridConfig", order = 0)]
    public class GridConfig : ScriptableObject
    {
        [SerializeField] [Min(0)] private float _cellSize = 0.8f;
        [SerializeField] private Vector2Int _mapSize = new Vector2Int(10, 10);
        [SerializeField] private LayerMask _buildingLayer;
        [SerializeField] private LayerMask _gridLayer;
        [SerializeField] private int _maxPathLength = 100;

        public float CellSize => _cellSize;
        public Vector2Int MapSize => _mapSize;
        public LayerMask BuildingLayer => _buildingLayer;
        public LayerMask GridLayer => _gridLayer;
        public int MaxPathLength => _maxPathLength;

        private void OnValidate()
        {
            if (_mapSize.x <= 0) _mapSize.x = 1;

            if (_mapSize.y <= 0) _mapSize.y = 1;
        }
    }
}