namespace Infrastructure.GameStateMachine
{
    public interface IEnterState : IExitState
    {
        void Enter();
    }
}