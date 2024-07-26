namespace Game.Gameplay.Components
{
    public sealed class Weapon
    {
        public float FireRadius { get; private set; }
        public float FireInterval { get; private set; }
        public float BulletSpeed { get; private set; }
        public int BulletDamage { get; private set; }
        public float CurrentFireInterval { get; set; }
        public void SetFireRadius(float fireRadius) => FireRadius = fireRadius;
        public void SetFireInterval(float fireInterval) => FireInterval = fireInterval;
        public void SetBulletSpeed(float bulletSpeed) => BulletSpeed = bulletSpeed;
        public void SetBulletDamage(int bulletDamage) => BulletDamage = bulletDamage;
    }
}