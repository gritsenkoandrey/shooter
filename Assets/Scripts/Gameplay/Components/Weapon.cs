using Core.Implementation;

namespace Gameplay.Components
{
    public sealed class Weapon : EntityComponent<Weapon>
    {
        public float RadiusSqr { get; set; }
        public float Interval { get; set; }
        public float Speed { get; set; }
        public int Damage { get; set; }
    }
}