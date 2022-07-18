using System;
using System.Collections.Generic;
using Core.Buildings;
using UnityEngine;

namespace Core.Grid
{
    [CreateAssetMenu(fileName = "InitialGridStateConfig", menuName = "Game Configs/InitialGridStateConfig",
        order = 0
    )]
    public class InitialGridStateConfig : ScriptableObject
    {
        [SerializeField] private List<BuildingData> _buildingsData = new List<BuildingData>();

        public List<BuildingData> BuildingsData => _buildingsData;
    }

    [Serializable]
    public struct BuildingData
    {
        public BuildingConfig Building;
        public Vector2Int[] OccupiedGridPosition;
        
        public BuildingData(BuildingConfig building, Vector2Int[] occupiedGridPosition)
        {
            Building = building;
            OccupiedGridPosition = occupiedGridPosition;
        }
    }
}