using System;
using System.Collections.Generic;
using Infrastructure.GameStateMachine.States;

namespace Infrastructure.GameStateMachine
{
    public sealed class GameStateMachine : IGameStateMachine
    {
        public IReadOnlyDictionary<Type, IExitState> States { get; }

        private IExitState _activeState;

        public GameStateMachine()
        {
            States = new Dictionary<Type, IExitState>
            {
                { typeof(BootstrapState), new BootstrapState(this) },
                { typeof(PrepareState), new PrepareState(this) },
                { typeof(LoadLevelState), new LoadLevelState(this) },
                { typeof(GameState), new GameState(this) },
                { typeof(ResultState), new ResultState(this) }
            };
        }

        void IGameStateMachine.Enter<TState>()
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        void IGameStateMachine.Enter<TState, TLoad>(TLoad load)
        {
            TState state = ChangeState<TState>();
            state.Enter(load);
        }

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState
        {
            return States[typeof(TState)] as TState;
        }
    }
}