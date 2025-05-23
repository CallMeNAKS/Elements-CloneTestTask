using _Elements.CodeBase.Gameplay.Grid;
using _Elements.CodeBase.Gameplay.Level;
using _Elements.CodeBase.Infrastructure.Service.Save;

namespace _Elements.CodeBase.Infrastructure.StateMachine.GlobalState
{
    public class ProcessingState : IGlobalState
    {
        private readonly GlobalStateMachine _stateMachine;
        private readonly SaveService _saveService;
        private readonly LevelService _levelService;
        private readonly GridCellService _gridCellService;

        public ProcessingState(SaveService saveService,
            GridCellService gridCellService,
            LevelService levelService)
        {
            _saveService = saveService;
            _gridCellService = gridCellService;
            _levelService = levelService;
        }

        public void Enter()
        {
            _saveService.Save(_levelService.CurrentLevel, _gridCellService.GetElementTypes());
        }

        public void Exit()
        {
        }
    }
}