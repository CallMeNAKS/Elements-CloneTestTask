using System.Collections.Generic;
using System.Threading;
using _Elements.CodeBase.Gameplay.Grid.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Elements.CodeBase.Gameplay.Grid
{
    public class GridElementDestroyer
    {
        private readonly GridCellService _cellService;

        public GridElementDestroyer(GridCellService cellService)
        {
            _cellService = cellService;
        }
        
        public async UniTask<bool> TryDestroyMatchedElements()
        {
            var positions = GetElementsToDestroy();
        
            if (positions.Count <= 0) 
                return false;

            var destroyTasks = new List<UniTask>();
            foreach (var pos in positions)
            {
                var cell = _cellService.GetCell(pos);
                if (cell?.GridElement != null)
                {
                    destroyTasks.Add(cell.GridElement.DestroyAsync());
                }
            }

            await UniTask.WhenAll(destroyTasks);

            return true;
        }

        private HashSet<Vector2Int> GetElementsToDestroy()
        {
            var toDestroy = new HashSet<Vector2Int>();
            int rows = _cellService.Rows;
            int cols = _cellService.Columns;

            for (int row = 0; row < rows; row++)
            {
                int count = 1;
                for (int col = 1; col < cols; col++)
                {
                    if (IsSameType(row, col, row, col - 1))
                        count++;
                    else
                        count = 1;

                    if (count >= 3)
                        for (int k = 0; k < count; k++)
                            toDestroy.Add(new Vector2Int(row, col - k));
                }
            }

            for (int col = 0; col < cols; col++)
            {
                int count = 1;
                for (int row = 1; row < rows; row++)
                {
                    if (IsSameType(row, col, row - 1, col))
                        count++;
                    else
                        count = 1;

                    if (count >= 3)
                        for (int k = 0; k < count; k++)
                            toDestroy.Add(new Vector2Int(row - k, col));
                }
            }

            var result = new HashSet<Vector2Int>(toDestroy);
            foreach (var pos in toDestroy)
            {
                var mainType = GetElementType(pos);
                foreach (var offset in _neighborOffsets)
                {
                    var neighbor = pos + offset;
                    if (_cellService.IsValidPosition(neighbor))
                    {
                        var neighborType = GetElementType(neighbor);
                        if (neighborType == mainType)
                        {
                            result.Add(neighbor);
                        }
                    }
                }
            }

            return result;
        }

        private bool IsSameType(int r1, int c1, int r2, int c2)
        {
            var type1 = GetElementType(new Vector2Int(r1, c1));
            var type2 = GetElementType(new Vector2Int(r2, c2));

            return type1 != GridElementType.Empty && type1 == type2;
        }

        private GridElementType GetElementType(Vector2Int pos)
        {
            var cell = _cellService.GetCell(pos);
            return cell?.GridElement?.GridElementType ?? GridElementType.Empty;
        }

        private static readonly Vector2Int[] _neighborOffsets =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };
    }
}