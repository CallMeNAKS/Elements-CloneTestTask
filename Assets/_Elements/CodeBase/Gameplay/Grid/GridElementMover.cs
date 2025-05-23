using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Elements.CodeBase.Gameplay.Grid
{
    public class GridElementMover
    {
        private readonly float _duration;
        private readonly GridCellService _gridCellService;

        public GridElementMover(GridCellService gridCellService, float duration = 0.25f)
        {
            _gridCellService = gridCellService;
            _duration = duration;
        }

        public async UniTask Swap(Vector2Int a, Vector2Int b)
        {
            if (!_gridCellService.IsValidPosition(a) || 
                !_gridCellService.IsValidPosition(b)) return;
    
            var cellA = _gridCellService.GetCell(a);
            var cellB = _gridCellService.GetCell(b);

            var rectA = cellA.RectTransform;
            var rectB = cellB.RectTransform;

            Vector2 posA = rectA.anchoredPosition;
            Vector2 posB = rectB.anchoredPosition;

            rectA.DOAnchorPos(posB, _duration).SetEase(Ease.InOutSine);
            rectB.DOAnchorPos(posA, _duration).SetEase(Ease.InOutSine);
            
            _gridCellService.SwapCellsData(a, b);
            _gridCellService.UpdateSortingOrder(a);
            _gridCellService.UpdateSortingOrder(b);
            
            await UniTask.WaitForSeconds(_duration);
        }
    }
}