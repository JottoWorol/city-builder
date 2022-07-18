using UnityEngine;

namespace Core.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider _touchCollider;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public BoxCollider TouchCollider => _touchCollider;
    }
}