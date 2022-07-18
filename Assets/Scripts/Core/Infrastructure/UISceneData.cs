using Core.Touch;
using UnityEngine;

namespace Core.Infrastructure
{
    public class UISceneData : MonoBehaviour
    {
        [SerializeField] private ScreenTouchView _screenTouchView;
        [SerializeField] private Transform _buildButtonParent;
        public ScreenTouchView ScreenTouchView => _screenTouchView;
        public Transform BuildButtonParent => _buildButtonParent;
    }
}