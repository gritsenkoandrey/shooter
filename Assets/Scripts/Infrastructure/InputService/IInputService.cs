namespace Game.Infrastructure.InputService
{
    public interface IInputService
    {
        float Horizontal { get;}
        float Vertical { get; }
        void Enable(bool isEnable);
        void Execute();
    }
}