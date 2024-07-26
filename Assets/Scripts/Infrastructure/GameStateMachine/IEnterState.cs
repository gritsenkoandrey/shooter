namespace Game.Infrastructure.GameStateMachine
{
    public interface IEnterState : IExitState
    {
        void Enter();
    }
}