using Core.Infrastructure;
using UnityEngine;
using Zenject;

namespace Core.Grid.Cells
{
    public class CellViewPool
    {
        private readonly CellView _cellViewPrefab;
        private readonly CellView[] _cellViews;
        private readonly Transform _cellViewPoolParent;

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