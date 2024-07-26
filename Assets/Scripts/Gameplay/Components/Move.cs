namespace Game.Gameplay.Components
{
    public sealed class Move
    {
        public float Speed { get; private set; }
        public void SetSpeed(float speed) => Speed = speed;
    }
}