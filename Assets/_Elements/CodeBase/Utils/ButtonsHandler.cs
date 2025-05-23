using _Elements.CodeBase.Gameplay.Level;
using _Elements.CodeBase.Infrastructure.StateMachine.GameplayState;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Elements.CodeBase
{
    public class ButtonsHandler : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _reloadButton;
        
        private GameplayStateMachine _gameplayStateMachine;
        private LevelService _levelService;

        [Inject]
        public void Construct(GameplayStateMachine gameplayStateMachine,
            LevelService levelService)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _levelService = levelService;
        }

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(OnNextClick);
            _reloadButton.onClick.AddListener(OnReloadClick);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveAllListeners();
            _reloadButton.onClick.RemoveAllListeners();
        }

        private void OnNextClick()
        {
            _levelService.LevelComplete();
            _gameplayStateMachine.ChangeState(GameplayStateName.Reload);
        }

        private void OnReloadClick()
        {
            _gameplayStateMachine.ChangeState(GameplayStateName.Reload);
        }
    }
}