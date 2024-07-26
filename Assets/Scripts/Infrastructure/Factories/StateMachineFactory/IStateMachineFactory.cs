using Game.Infrastructure.GameStateMachine;

namespace Game.Infrastructure.Factories.StateMachineFactory
{
    public interface IStateMachineFactory
    {
        IGameStateMachine CreateGameStateMachine();
    }
}