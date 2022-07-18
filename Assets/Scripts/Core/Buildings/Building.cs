using System.Collections.Generic;
using Core.Character;
using UnityEngine;

namespace Core.Buildings
{
    public class Building
    {
        public readonly List<GridCharacter> AttachedCharacters = new List<GridCharacter>();

        public Building(BuildingView buildingView, BuildingConfig buildingConfig)
        {
            BuildingView = buildingView;
            BuildingConfig = buildingConfig;
        }

        public BuildingConfig BuildingConfig { get; }
        public Vector2Int[] OccupiedCellPositions { get; private set; }
        public BuildingView BuildingView { get; }

        public void AttachCharacter(GridCharacter character)
        {
            AttachedCharacters.Add(character);
        }

        public void SetOccupiedCellPositions(Vector2Int[] occupiedCellPositions)
        {
            OccupiedCellPositions = occupiedCellPositions;
        }

        public void Destroy()
        {
            Object.Destroy(BuildingView.gameObject);
        }

        public void SetPosition(Vector3 position) => BuildingView.transform.position = position;
    }
}