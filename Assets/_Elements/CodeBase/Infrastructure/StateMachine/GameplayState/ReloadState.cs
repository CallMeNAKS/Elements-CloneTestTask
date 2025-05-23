namespace _Elements.CodeBase.Infrastructure.StateMachine.GameplayState
{
    public class ReloadState : IGameplayState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;

        public ReloadState(GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void Enter()
        {
            _gameplayStateMachine.ChangeState(GameplayStateName.LoadGame);
        }

        public void Exit()
        {
        }
    }
}