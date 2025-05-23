using System.Collections.Generic;
using _Elements.CodeBase.Gameplay.Grid.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Elements.CodeBase.Gameplay.Grid
{
    public class GridFieldNormalizer
    {
        private readonly GridElementMover _gridElementMover;
        private readonly GridCellService _gridCellService;

        public GridFieldNormalizer(GridElementMover gridElementMover,
            GridCellService gridCellService)
        {
            _gridElementMover = gridElementMover;
            _gridCellService = gridCellService;
        }

        public async UniTask Normalize()
        {
            int rows = _gridCellService.Rows;
            int cols = _gridCellService.Columns;
            var moveTasks = new List<UniTask>();

            for (int col = 0; col < cols; col++)
            {
                for (int row = 1; row < rows; row++)
                {
                    var pos = new Vector2Int(row, col);
                    var cell = _gridCellService.GetCell(pos);

                    if (cell?.GridElement == null || cell.GridElement.GridElementType == GridElementType.Empty)
                        continue;

                    int targetRow = row;
                    for (int r = row - 1; r >= 0; r--)
                    {
                        var checkPos = new Vector2Int(r, col);
                        var checkCell = _gridCellService.GetCell(checkPos);

                        if (checkCell?.GridElement == null ||
                            checkCell.GridElement.GridElementType == GridElementType.Empty)
                        {
                            targetRow = r;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (targetRow != row)
                    {
                        moveTasks.Add(_gridElementMover.Swap(
                            new Vector2Int(row, col),
                            new Vector2Int(targetRow, col)
                        ));
                    }
                }
            }

            if (moveTasks.Count > 0)
            {
                await UniTask.WhenAll(moveTasks);
            }
        }
    }
}