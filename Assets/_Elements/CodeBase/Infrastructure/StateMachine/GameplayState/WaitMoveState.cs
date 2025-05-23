using _Elements.CodeBase.Gameplay.Grid;
using _Elements.CodeBase.Gameplay.Level;
using _Elements.CodeBase.Infrastructure.Service.Save;
using _Elements.CodeBase.Utils;
using _Elements.CodeBase.Visual;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Elements.CodeBase.Infrastructure.StateMachine.GameplayState
{
    public class WaitMoveState : IGameplayState
    {
        private readonly GridElementMover _gridElementMover;
        private readonly GridSwapProvider _gridSwapProvider;
        private readonly GameplayStateMachine _stateMachine;
        private readonly InputDisabler _inputDisabler;
        private readonly BalloonService _balloonService;
        
        private readonly SaveService _saveService;
        private readonly LevelService _levelService;
        private readonly GridCellService _gridCellService;

        public WaitMoveState(GridElementMover mover, 
            GridSwapProvider swapProvider,
            GameplayStateMachine stateMachine,
            InputDisabler inputDisabler,
            BalloonService balloonService,
            SaveService saveService,
            LevelService levelService,
            GridCellService gridCellService)
        {
            _gridElementMover = mover;
            _gridSwapProvider = swapProvider;
            _stateMachine = stateMachine;
            _inputDisabler = inputDisabler;
            _balloonService = balloonService;
            _saveService = saveService;
            _levelService = levelService;
            _gridCellService = gridCellService;
        }

        public async void Enter()
        {
            _saveService.Save(_levelService.CurrentLevel, _gridCellService.GetElementTypes());
            
            _gridSwapProvider.OnSwapRequested += HandleSwap;
            _inputDisabler.EnableInput();
            await _balloonService.SpawnBalloons();
        }

        public void Exit()
        {
            _gridSwapProvider.OnSwapRequested -= HandleSwap;
        }

        private async UniTask HandleSwap(Vector2Int from, Vector2Int to)
        {
            _inputDisabler.DisableInput();
            await _gridElementMover.Swap(from, to);
            _stateMachine.ChangeState(GameplayStateName.MoveProcessing);
        }
    }
}