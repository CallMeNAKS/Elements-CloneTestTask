using System.Collections.Generic;

namespace _Elements.CodeBase.Infrastructure.StateMachine
{
    public abstract class StateMachine<TStateName, TState> where TState : class, IState
    {
        private Dictionary<TStateName, TState> _registeredStates = new();
        private TState _currentState;

        public void ChangeState(TStateName stateName)
        {
            if (_registeredStates[stateName] == _currentState) return;

            TState newState = _registeredStates[stateName];

             _currentState?.Exit();
             
             _currentState = newState;
            newState.Enter();
        }

        public void RegisterState(TStateName stateName, TState state)
        {
            _registeredStates[stateName] = state;
        }

        public abstract void Init(); 
    }
}