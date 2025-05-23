using _Elements.CodeBase.Gameplay.Grid.Data;
using UnityEngine;

namespace _Elements.CodeBase.Gameplay.Grid
{
    public class GridCellService
    {
        private GridCell[,] _cells;
        private int _rows;
        private int _columns;
        private bool _isInitialized;

        public GridCell[,] Cells => _cells;
        public int Rows => _rows;
        public int Columns => _columns;

        public void Initialize(GridCell[,] cells)
        {
            _cells = cells;
            _rows = cells.GetLength(0);
            _columns = cells.GetLength(1);
            
            _isInitialized = true;
        }

        public bool IsValidPosition(Vector2Int pos)
        {
            return pos.x >= 0 && pos.x < _rows &&
                   pos.y >= 0 && pos.y < _columns;
        }

        public GridCell GetCell(Vector2Int pos)
        {
            return IsValidPosition(pos) ? _cells[pos.x, pos.y] : null;
        }

        public void SwapCellsData(Vector2Int a, Vector2Int b)
        {
            if (!IsValidPosition(a) || !IsValidPosition(b)) return;
            
            var cellA = _cells[a.x, a.y];
            var cellB = _cells[b.x, b.y];

            _cells[a.x, a.y] = cellB;
            _cells[b.x, b.y] = cellA;

            if (cellA != null) cellA.SetGridPosition(b);
            if (cellB != null) cellB.SetGridPosition(a);
        }

        public void UpdateSortingOrder(Vector2Int pos)
        {
            if (!IsValidPosition(pos)) return;
            
            var cell = _cells[pos.x, pos.y];
            if (cell != null)
            {
                cell.transform.SetSiblingIndex(pos.x * _columns + pos.y);
            }
        }
        
        public bool IsCellEnded()
        {
            for (int x = 0; x < _rows; x++)
            {
                for (int y = 0; y < _columns; y++)
                {
                    var cell = _cells[x, y];
                    if (cell != null && 
                        cell.GridElement != null && 
                        cell.GridElement.GridElementType != GridElementType.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public GridElementType[,] GetElementTypes()
        {
            var result = new GridElementType[_rows, _columns];
            for (int x = 0; x < _rows; x++)
            {
                for (int y = 0; y < _columns; y++)
                {
                    var cell = _cells[x, y];
                    result[x, y] = cell?.GridElement?.GridElementType ?? GridElementType.Empty;
                }
            }

            return result;
        }
    }
}