using _Elements.CodeBase.Gameplay.Grid.Data;
using _Elements.CodeBase.Gameplay.Level;
using _Elements.CodeBase.Infrastructure.Service.Save;
using UnityEngine;

namespace _Elements.CodeBase.Infrastructure.StateMachine.GlobalState
{
    public class InitializeState : IGlobalState
    {
        private readonly GlobalStateMachine _globalStateMachine;
        private readonly SaveService _saveService;
        private readonly LevelService _levelService;
        private readonly GridFieldData _gridFieldData;

        public InitializeState(GlobalStateMachine globalStateMachine,
            SaveService saveService,
            LevelService levelService)
        {
            _globalStateMachine = globalStateMachine;
            _saveService = saveService;
            _levelService = levelService;
        }

        public void Enter()
        {
            var save = _saveService.Load();
            if (save != null)
            {
                _levelService.LoadLevel(save.currentLevel, save.gridWrapper.To2DArray());
            }
         
            _globalStateMachine.ChangeState(GlobalStateName.Gameplay);
        }

        public void Exit()
        {
        }
    }
}