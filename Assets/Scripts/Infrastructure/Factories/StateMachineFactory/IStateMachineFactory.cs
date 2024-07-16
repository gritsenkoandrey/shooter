using Infrastructure.GameStateMachine;

namespace Infrastructure.Factories.StateMachineFactory
{
    public interface IStateMachineFactory
    {
        IGameStateMachine CreateGameStateMachine();
    }
}