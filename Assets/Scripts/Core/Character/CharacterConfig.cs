using UnityEngine;

namespace Core.Character
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Game Configs/CharacterConfig", order = 0)]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private CharacterView _characterView;

        public float MoveSpeed => _moveSpeed;
        public CharacterView CharacterView => _characterView;
    }
}