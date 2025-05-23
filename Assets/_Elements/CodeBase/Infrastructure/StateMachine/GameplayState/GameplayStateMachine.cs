namespace _Elements.CodeBase.Infrastructure.StateMachine.GameplayState
{
    public enum GameplayStateName
    {
        LoadGame = 0,
        WaitMove = 1,
        MoveProcessing = 2,
        Reload = 3,
    }

    public class GameplayStateMachine : StateMachine<GameplayStateName, IGameplayState>
    {
        private readonly StateFactory _stateFactory;

        public GameplayStateMachine(StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public override void Init()
        {
            RegisterState(GameplayStateName.LoadGame, _stateFactory.CreateState<LoadGameState>());
            RegisterState(GameplayStateName.WaitMove, _stateFactory.CreateState<WaitMoveState>());
            RegisterState(GameplayStateName.MoveProcessing, _stateFactory.CreateState<MoveProcessingState>());
            RegisterState(GameplayStateName.Reload, _stateFactory.CreateState<ReloadState>());

            ChangeState(GameplayStateName.LoadGame);
        }
    }
}