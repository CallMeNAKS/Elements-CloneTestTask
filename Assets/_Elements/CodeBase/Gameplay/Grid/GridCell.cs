using UnityEngine;
using UnityEngine.EventSystems;

namespace _Elements.CodeBase.Gameplay.Grid
{
    public class GridCell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        
        private Vector2 _startPointerPos;
        private GridSwapProvider _provider;
        
        [field: SerializeField] public RectTransform RectTransform  { get; private set; }
        public Vector2Int GridPosition { get; private set; }
        public GridElement GridElement { get; private set; } 

        public void Init(Vector2Int position, GridSwapProvider provider, GridElement gridElement)
        {
            GridPosition = position;
            _provider = provider;
            GridElement = gridElement;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _startPointerPos = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Vector2 endPos = eventData.position;
            Vector2 delta = endPos - _startPointerPos;

            if (delta.sqrMagnitude < 300f) return;

            Vector2Int direction = Vector2Int.zero;
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                direction = delta.x > 0 ? Vector2Int.up : Vector2Int.down;
            else
                direction = delta.y > 0 ? Vector2Int.right : Vector2Int.left; 

            _provider.RequestSwap(this, direction);
        }

        public void SetGridPosition(Vector2Int newPos)
        {
            GridPosition = newPos;
        }
    }
}