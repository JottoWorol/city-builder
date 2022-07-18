using Core.Grid;
using Core.Infrastructure;
using UnityEngine;

namespace Core.Character
{
    public class CharacterFactory
    {
        private readonly CharacterConfig _characterConfig;
        private readonly CharacterContainer _characterContainer;
        private readonly Transform _characterParent;
        private readonly GridCellContainer _gridCellContainer;
        private readonly GridNavigation _gridNavigation;

        public CharacterFactory(StaticData staticData, GridCellContainer gridCellContainer, SceneData sceneData,
            GridNavigation gridNavigation, CharacterContainer characterContainer)
        {
            _gridCellContainer = gridCellContainer;
            _gridNavigation = gridNavigation;
            _characterContainer = characterContainer;
            _characterConfig = staticData.DefaultCharacterConfig;
            _characterParent = sceneData.CharacterParent;
        }

        public GridCharacter SpawnCharacterAt(Vector2Int position)
        {
            var view = Object.Instantiate(_characterConfig.CharacterView, _characterParent);
            var gridCell = _gridCellContainer.GetCellByPosition(position);
            view.transform.position = gridCell.WorldPosition;

            var character = new GridCharacter(view, _gridNavigation, _gridCellContainer, _characterConfig, position);
            var animation = new CharacterAnimation(view.Animator);
            var directionSwitch = new CharacterDirectionSwitch(character, animation);

            _characterContainer.AddCharacter(character);
            return character;
        }
    }
}