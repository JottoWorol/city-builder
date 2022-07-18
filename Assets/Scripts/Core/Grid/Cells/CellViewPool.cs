using Core.Infrastructure;
using UnityEngine;

namespace Core.Grid.Cells
{
    public class CellViewPool
    {
        private readonly Transform _cellViewPoolParent;
        private readonly CellView _cellViewPrefab;
        private readonly CellView[] _cellViews;

        public CellViewPool(SceneData sceneData, StaticData staticData)
        {
            _cellViewPrefab = staticData.CellViewPrefab;


            _cellViewPoolParent = sceneData.CellViewPoolParent;
            _cellViews = _cellViewPoolParent.GetComponentsInChildren<CellView>();
        }

        public CellView GetCellView()
        {
            foreach (var cellView in _cellViews)
            {
                if (!cellView.IsActive)
                {
                    cellView.IsActive = true;
                    return cellView;
                }
            }

            var newCellView = Object.Instantiate(_cellViewPrefab, _cellViewPoolParent);
            return newCellView;
        }

        public void ReturnCellView(CellView cellView)
        {
            cellView.IsActive = false;
        }
    }
}