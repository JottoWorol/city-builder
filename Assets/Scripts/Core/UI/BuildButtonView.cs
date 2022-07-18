using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class BuildButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _iconImage;

        public Button Button => _button;
        public Image IconImage => _iconImage;
    }
}