using System;
using _Elements.CodeBase.Gameplay.Grid.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Elements.CodeBase.Gameplay.Grid
{
    public class GridSwapProvider
    {
        private readonly GridCellService _cellService;
        
        public event Func<Vector2Int, Vector2Int, UniTask> OnSwapRequested;

        public GridSwapProvider(GridCellService cellService)
        {
            _cellService = cellService;
        }

        public void RequestSwap(GridCell element, Vector2Int direction)
        {
            if (element == null) return;
            
            Vector2Int from = element.GridPosition;
            Vector2Int to = from + direction;

            if (CanSwap(from, to))
            {
                _ = OnSwapRequested?.Invoke(from, to);
            }
        }

        private bool CanSwap(Vector2Int from, Vector2Int to)
        {
            if (!_cellService.IsValidPosition(to))
                return false;

            if (to.x > from.x)
                if( _cellService.GetCell(to).GridElement == null 
                    || _cellService.GetCell(to).GridElement.GridElementType == GridElementType.Empty)
                    return false;
            
            return true;
        }
    }
}