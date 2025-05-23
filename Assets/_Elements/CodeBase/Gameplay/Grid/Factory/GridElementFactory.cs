using _Elements.CodeBase.Gameplay.Grid.Data;
using UnityEngine;

namespace _Elements.CodeBase.Gameplay.Grid.Factory
{
    public class GridElementFactory : IGridElementFactory
    {
        private readonly GridElement _waterPrefab = Resources.Load<GridElement>(WaterPrefabPath);
        private readonly GridElement _firePrefab = Resources.Load<GridElement>(FirePrefabPath);
        private readonly GridCell _gridCellPrefab = Resources.Load<GridCell>(GridCellPrefabPath);
        
        private GridSwapProvider _swapProvider;

        private const string WaterPrefabPath = "Prefab/GridElement/WaterElement";
        private const string FirePrefabPath = "Prefab/GridElement/FireElement";
        private const string GridCellPrefabPath = "Prefab/GridElement/GridCell";

        public GridElementFactory(GridSwapProvider swapProvider)
        {
            _swapProvider = swapProvider;
        }

        public GridCell Create(GridElementType type, Vector2Int position)
        {
            var gridElement = Object.Instantiate(_gridCellPrefab);
            GridElement element = null; 

            if (type == GridElementType.Water)
            {
                element = CreateView(_waterPrefab, gridElement);
            }
            else if (type == GridElementType.Fire)
            {
                element = CreateView(_firePrefab, gridElement);
            }
            
            gridElement.Init(position, _swapProvider, element);

            return gridElement;
        }

        private GridElement CreateView(GridElement prefab, GridCell gridElement)
        {
            var element = Object.Instantiate(prefab, gridElement.transform);
            element.transform.localScale = Vector3.one * 1.5f;
            element.transform.localPosition += new Vector3(15, 0, 0);
            return element;
        }
    }
}