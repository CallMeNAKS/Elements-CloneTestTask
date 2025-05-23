using _Elements.CodeBase.Gameplay.Grid;
using _Elements.CodeBase.Gameplay.Grid.Data;
using _Elements.CodeBase.Gameplay.Grid.Factory;
using _Elements.CodeBase.Gameplay.Level;
using _Elements.CodeBase.Infrastructure.Service.Save;
using _Elements.CodeBase.Infrastructure.StateMachine;
using _Elements.CodeBase.Infrastructure.StateMachine.GameplayState;
using _Elements.CodeBase.Infrastructure.StateMachine.GlobalState;
using _Elements.CodeBase.Utils;
using _Elements.CodeBase.Visual;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace _Elements.CodeBase.Infrastructure.Installer
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private InputDisabler _inputDisabler;
        [SerializeField] private Transform _environmentContainer;

        public override void InstallBindings()
        {
            RegisterGlobalStateMachine();
            RegisterGameStateMachine();
            RegisterGridServices();
            RegisterInputDisabler();
            RegisterBalloonServices();
            RegisterLevelService();
            RegisterSaveService();
        }

        private void RegisterGlobalStateMachine()
        {
            Container.Bind<StateFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<GlobalStateMachine>().AsSingle();

            Container.BindInterfacesAndSelfTo<InitializeState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProcessingState>().AsSingle();
        }

        private void RegisterGameStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameplayStateMachine>().AsSingle();

            Container.BindInterfacesAndSelfTo<LoadGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<WaitMoveState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ReloadState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MoveProcessingState>().AsSingle();
        }

        private void RegisterGridServices()
        {
            Container.Bind<GridGenerator>().AsSingle().WithArguments(_gridLayoutGroup);
            Container.Bind<GridFieldData>().AsSingle();
            Container.Bind<GridElementMover>().AsSingle();
            Container.Bind<GridSwapProvider>().AsSingle();
            Container.Bind<IGridElementFactory>().To<GridElementFactory>().AsSingle();
            Container.Bind<GridFieldNormalizer>().AsSingle();
            Container.Bind<GridFiller>().AsSingle().WithArguments(_gridLayoutGroup.transform);
            Container.Bind<GridElementDestroyer>().AsSingle();
            Container.Bind<GridCellService>().AsSingle();
        }

        private void RegisterInputDisabler()
        {
            Container.Bind<InputDisabler>().FromInstance(_inputDisabler).AsSingle();
        }

        private void RegisterBalloonServices()
        {
            Container.Bind<BalloonService>().AsSingle().WithArguments(_environmentContainer);
        }

        private void RegisterLevelService()
        {
            Container.Bind<LevelService>().AsSingle();
        }

        private void RegisterSaveService()
        {
            Container.Bind<SaveService>().AsSingle();
        }
    }
}