using System;

namespace Core.Character
{
    public class CharacterDirectionSwitch
    {
        private readonly CharacterAnimation _characterAnimation;
        private readonly GridCharacter _gridCharacter;
        private HorizontalDirection _currentHorizontalDirection;

        private VerticalDirection _currentVerticalDirection;

        public CharacterDirectionSwitch(GridCharacter gridCharacter, CharacterAnimation characterAnimation)
        {
            _gridCharacter = gridCharacter;
            _characterAnimation = characterAnimation;

            _gridCharacter.DirectionChanged += OnDirectionChanged;
            _gridCharacter.Destroyed += OnCharacterDestroyed;
        }

        private void OnCharacterDestroyed()
        {
            _gridCharacter.DirectionChanged -= OnDirectionChanged;
        }

        private void OnDirectionChanged()
        {
            var isBottom = _gridCharacter.CurrentDirection.y < 0 || _gridCharacter.CurrentDirection.x < 0;
            var newVerticalDirection
                = isBottom ? VerticalDirection.Bottom : VerticalDirection.Top;

            var isRight = _gridCharacter.CurrentDirection.y < 0 || _gridCharacter.CurrentDirection.x > 0;
            var newHorizontalDirection
                = isRight ? HorizontalDirection.Right : HorizontalDirection.Left;

            if (newHorizontalDirection != _currentHorizontalDirection)
            {
                _currentHorizontalDirection = newHorizontalDirection;
                _gridCharacter.SetSpriteMirror(_currentHorizontalDirection == HorizontalDirection.Right);
            }

            if (newVerticalDirection != _currentVerticalDirection)
            {
                _currentVerticalDirection = newVerticalDirection;

                switch (newVerticalDirection)
                {
                    case VerticalDirection.Top:
                        _characterAnimation.PlayRunTop();
                        break;
                    case VerticalDirection.Bottom:
                        _characterAnimation.PlayRunBottom();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private enum VerticalDirection
        {
            Top,
            Bottom,
        }

        private enum HorizontalDirection
        {
            Left,
            Right,
        }
    }
}