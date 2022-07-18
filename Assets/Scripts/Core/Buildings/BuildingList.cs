using System.Collections.Generic;
using UnityEngine;

namespace Core.Buildings
{
    [CreateAssetMenu(fileName = "BuildingList", menuName = "Game Configs/BuildingList", order = 0)]
    public class BuildingList : ScriptableObject
    {
        [SerializeField] private List<BuildingConfig> _buildings = new List<BuildingConfig>();

        public List<BuildingConfig> Buildings => _buildings;
    }
}