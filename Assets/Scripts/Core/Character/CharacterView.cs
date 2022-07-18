using UnityEngine;

namespace Core.Character
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Animator Animator => _animator;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
    }
}