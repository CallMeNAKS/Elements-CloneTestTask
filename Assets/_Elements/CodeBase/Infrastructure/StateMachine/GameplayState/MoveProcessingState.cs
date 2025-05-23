using _Elements.CodeBase.Gameplay.Grid;
using _Elements.CodeBase.Gameplay.Level;
using UnityEngine;

namespace _Elements.CodeBase.Infrastructure.StateMachine.GameplayState
{
    public class MoveProcessingState : IGameplayState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly GridFieldNormalizer _gridNormalizer;
        private readonly GridElementDestroyer _gridElementDestroyer;
        private readonly GridCellService _gridCellService;
        private readonly LevelService _levelService;

        public MoveProcessingState(GridFieldNormalizer gridNormalizer,
            GameplayStateMachine gameplayStateMachine,
            GridElementDestroyer gridElementDestroyer,
            GridCellService gridCellService, 
            LevelService levelService)
        {
            _gridNormalizer = gridNormalizer;
            _gameplayStateMachine = gameplayStateMachine;
            _gridElementDestroyer = gridElementDestroyer;
            _gridCellService = gridCellService;
            _levelService = levelService;
        }

        public async void Enter()
        {
            bool isDestroy;
            
            int maxIterations = 10;
            int iterations = 0;
            
            do
            {
                await _gridNormalizer.Normalize();
                isDestroy = await _gridElementDestroyer.TryDestroyMatchedElements();
                iterations++;
            } while (isDestroy && iterations < maxIterations);

            if (_gridCellService.IsCellEnded())
            {
                _levelService.LevelComplete();
                _gameplayStateMachine.ChangeState(GameplayStateName.Reload);
                return;
            }
            
            _gameplayStateMachine.ChangeState(GameplayStateName.WaitMove);
        }

        public void Exit()
        {
        }
    }
}