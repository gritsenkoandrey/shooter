namespace Game.Infrastructure.GameStateMachine
{
    public interface IEnterLoadState<in TLoad> : IExitState
    {
        void Enter(TLoad load);
    }
}