using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Elements.CodeBase.Gameplay.Grid
{
    public class GridGenerator
    {
        private readonly GridLayoutGroup _gridLayout;
        private readonly RectTransform _rectTransform;
        
        private const float CellMaxSize = 200f;

        public GridGenerator(GridLayoutGroup gridLayout)
        {
            _gridLayout = gridLayout;
            _rectTransform = gridLayout.GetComponent<RectTransform>();
        }

        public void Generate(int rows, int columns)
        {
            Vector2 totalSize = _rectTransform.rect.size;
            RectOffset padding = _gridLayout.padding;
            
            float availableWidth = totalSize.x - padding.left - padding.right;
            float availableHeight = totalSize.y - padding.top - padding.bottom;

            float cellWidth = availableWidth / columns;
            float cellHeight = availableHeight / rows;
            float cellSize = Mathf.Min(cellWidth, cellHeight);
            cellSize = Mathf.Min(cellSize, CellMaxSize);

            cellSize = RoundToNearest(cellSize, 5f);

            _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayout.constraintCount = columns;
            _gridLayout.cellSize = new Vector2(cellSize, cellSize);
            _gridLayout.spacing = Vector2.zero;
        }

        public async UniTask RefreshLayout()
        {
            _gridLayout.enabled = true;
            
            await UniTask.DelayFrame(2);
            
            _gridLayout.enabled = false;
        }

        private float RoundToNearest(float value, float nearest)
        {
            return Mathf.Round(value / nearest) * nearest;
        }
    }
}