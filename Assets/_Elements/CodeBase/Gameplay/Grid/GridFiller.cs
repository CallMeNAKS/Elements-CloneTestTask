using _Elements.CodeBase.Gameplay.Grid.Data;
using _Elements.CodeBase.Gameplay.Grid.Factory;
using UnityEngine;

namespace _Elements.CodeBase.Gameplay.Grid
{
    public class GridFiller
    {
        private readonly Transform _container;
        private readonly IGridElementFactory _factory;

        public GridFiller(Transform container, IGridElementFactory factory)
        {
            _container = container;
            _factory = factory;
        }

        public GridCell[,] Fill(GridFieldData data)
        {
            Clear();

            int rows = data.Rows;
            int cols = data.Columns;
            GridCell[,] cells = new GridCell[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var type = data.GetAt(row, col);
                    var position = new Vector2Int(row, col);

                    var cell = _factory.Create(type, position); // теперь это MonoBehaviour
                    cell.transform.SetParent(_container, false);

                    cells[row, col] = cell;
                }
            }
            
            return cells;
        }

        private void Clear()
        {
            for (int i = _container.childCount - 1; i >= 0; i--)
                Object.Destroy(_container.GetChild(i).gameObject);
        }
    }
}