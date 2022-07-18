using UnityEngine;

namespace Core.Grid.Cells
{
    public class CellView : MonoBehaviour
    {
        public Transform Transform => transform;

        public bool IsActive
        {
            get => gameObject.activeInHierarchy;
            set => gameObject.SetActive(value);
        }
    }
}