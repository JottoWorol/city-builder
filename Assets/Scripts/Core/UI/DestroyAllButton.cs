﻿using System;
using Core.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Core.UI
{
    public class DestroyAllButton : IDisposable
    {
        private Button _button;
        private readonly Button _buttonPrefab;
        private readonly Transform _buttonParent;

        public DestroyAllButton(StaticData staticData, UISceneData uiSceneData)
        {
            _buttonPrefab = staticData.DestroyAllButtonPrefab;
            _buttonParent = uiSceneData.BuildButtonParent;
        }

        public void Initialize()
        {
            _button = Object.Instantiate(_buttonPrefab, _buttonParent);
            _button.onClick.AddListener(OnClick);
        }
        
        public event Action ButtonClicked;

        private void OnClick()
        {
            ButtonClicked?.Invoke();
        }

        public void Dispose()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}