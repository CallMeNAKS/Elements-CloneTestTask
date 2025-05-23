using _Elements.CodeBase.Gameplay.Grid.Data;
using UnityEngine;

namespace _Elements.CodeBase.Gameplay.Grid.Factory
{
    public interface IGridElementFactory
    {
        public GridCell Create(GridElementType type, Vector2Int position);
    }
}