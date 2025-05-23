using _Elements.CodeBase.Infrastructure.StateMachine.GlobalState;
using UnityEngine;
using Zenject;

namespace _Elements.CodeBase.Infrastructure
{
    [DefaultExecutionOrder(-999)] //Упрощение, вместо создания отдельной сцены c bootstrap классом
    public class EntryPoint : MonoBehaviour
    {
        private GlobalStateMachine _globalStateMachine;

        [Inject]
        public void Construct(GlobalStateMachine globalStateMachine)
        {
            _globalStateMachine = globalStateMachine;
        }
        
        private void Awake()
        {
            _globalStateMachine.Init();
        }
    }
}