using System;
using _Elements.CodeBase.Gameplay.Grid.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Elements.CodeBase.Gameplay.Grid
{
    public class GridElement : MonoBehaviour
    {
        private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");
        
        [SerializeField] private Image _image;
        [SerializeField] private Animator _animator;
        
        [field: SerializeField] public GridElementType GridElementType { get; private set; }

        public event Action OnDestroyed;

        public UniTask DestroyAsync()
        {
            var completionSource = new UniTaskCompletionSource();
        
            void OnDestroyedHandler()
            {
                OnDestroyed -= OnDestroyedHandler;
                completionSource.TrySetResult();
            }

            OnDestroyed += OnDestroyedHandler;
            GridElementType = GridElementType.Empty;
            _animator.SetTrigger(DestroyTrigger);

            return completionSource.Task;
        }

        public void OnDestroyAnimationFinished()
        {
            _image.enabled = false;
            OnDestroyed?.Invoke();
        }
    }
}