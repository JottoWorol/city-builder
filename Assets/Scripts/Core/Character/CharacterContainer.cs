using System.Collections.Generic;
using Zenject;

namespace Core.Character
{
    public class CharacterContainer : ITickable
    {
        private readonly List<GridCharacter> _characters = new List<GridCharacter>();

        public void Tick()
        {
            foreach (var character in _characters)
            {
                character.Update();
            }
        }

        public void AddCharacter(GridCharacter character)
        {
            _characters.Add(character);
        }

        public void DestroyCharacter(GridCharacter gridCharacter)
        {
            gridCharacter.Destroy();
            _characters.Remove(gridCharacter);
        }
    }
}