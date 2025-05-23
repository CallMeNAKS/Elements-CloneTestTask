using _Elements.CodeBase.Gameplay.Grid;
using _Elements.CodeBase.Gameplay.Grid.Data;
using _Elements.CodeBase.Gameplay.Level;
using UnityEngine;

namespace _Elements.CodeBase.Infrastructure.StateMachine.GameplayState
{
    public class LoadGameState : IGameplayState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly GridFiller _gridFiller;
        private readonly GridGenerator _gridGenerator;
        private readonly GridFieldData _gridFieldData;
        private readonly GridCellService _gridCellService;
        private readonly LevelService _levelService;

        public LoadGameState(
            GameplayStateMachine gameplayStateMachine,
            GridFiller gridFiller,
            GridGenerator gridGenerator,
            GridFieldData gridFieldData, 
            GridCellService gridCellService,
            LevelService levelService) 
        {
            _gameplayStateMachine = gameplayStateMachine;
            _gridFiller = gridFiller;
            _gridGenerator = gridGenerator;
            _gridFieldData = gridFieldData;
            _gridCellService = gridCellService;
            _levelService = levelService;
        }

        public async void Enter()
        {
            var level = _levelService.GetCurrentLevel();
            _gridFieldData.LoadLevel(level);
            _gridGenerator.Generate(_gridFieldData.Rows, _gridFieldData.Columns);
            GridCell[,] cells = _gridFiller.Fill(_gridFieldData);
            await _gridGenerator.RefreshLayout();
            
            _gridCellService.Initialize(cells);
            
            _gameplayStateMachine.ChangeState(GameplayStateName.WaitMove);
        }

        public void Exit()
        {
        }
    }
}