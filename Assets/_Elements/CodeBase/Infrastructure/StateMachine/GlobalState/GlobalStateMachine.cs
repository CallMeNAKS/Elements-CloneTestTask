namespace _Elements.CodeBase.Infrastructure.StateMachine.GlobalState
{
    public enum GlobalStateName
    {
        Initialize = 0,
        Gameplay = 1,
        Processing = 2,
    }

    public class GlobalStateMachine : StateMachine<GlobalStateName, IGlobalState>
    {
        private readonly StateFactory _stateFactory;

        public GlobalStateMachine(StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public override void Init()
        {
            RegisterState(GlobalStateName.Initialize, _stateFactory.CreateState<InitializeState>());
            RegisterState(GlobalStateName.Gameplay, _stateFactory.CreateState<GameplayState>());
            RegisterState(GlobalStateName.Processing, _stateFactory.CreateState<ProcessingState>());

            ChangeState(GlobalStateName.Initialize);
        }
    }
}