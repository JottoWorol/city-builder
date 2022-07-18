using UnityEngine;

namespace Core.Grid
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private Transform _centerPoint;

        public Transform CenterPoint => _centerPoint;
    }
}