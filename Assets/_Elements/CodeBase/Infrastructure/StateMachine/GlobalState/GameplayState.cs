using _Elements.CodeBase.Infrastructure.StateMachine.GameplayState;
using UnityEngine;

namespace _Elements.CodeBase.Infrastructure.StateMachine.GlobalState
{
    public class GameplayState : IGlobalState
    {
        private GameplayStateMachine _gameplayStateMachine;

        public GameplayState(GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void Enter()
        {
            _gameplayStateMachine.Init();
        }

        public void Exit()
        {
        }
    }
}