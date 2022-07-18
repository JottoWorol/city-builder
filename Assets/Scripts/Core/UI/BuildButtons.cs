using System;
using System.Collections.Generic;
using Core.Buildings;
using Core.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Core.UI
{
    public class BuildButtons : IDisposable
    {
        private readonly BuildingList _buildingList;
        private readonly BuildButtonView _buttonPrefab;
        private readonly Transform _buttonParent;

        private readonly List<Button> _buttons = new List<Button>();
        
        public BuildButtons(StaticData staticData, UISceneData uiSceneData)
        {
            _buttonPrefab = staticData.BuildViewButtonPrefab;
            _buildingList = staticData.BuildingList;
            _buttonParent = uiSceneData.BuildButtonParent;
        }

        public event Action<BuildingConfig> ClickedForBuild;

        public void Initialize()
        {
            foreach (var building in _buildingList.Buildings)
            {
                var button = Object.Instantiate(_buttonPrefab, _buttonParent);
                button.IconImage.sprite = building.Icon;
                button.Button.onClick.AddListener(() => ClickedForBuild?.Invoke(building));
                _buttons.Add(button.Button);
            }
        }

        public void Dispose()
        {
            foreach (var button in _buttons)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}