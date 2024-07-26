namespace Game.Core.Implementation
{
    public abstract class EntityComponent<T> : Entity where T : Entity
    {
        private void Awake()
        {
            OnEntityCreate();
        }

        private void OnEnable()
        {
            OnEntityEnable();
            
            EntityBridge<T>.Registered(this);
        }

        private void OnDisable()
        {
            OnEntityDisable();

            EntityBridge<T>.Unregistered(this);
        }

        private void OnDestroy()
        {
            OnEntityDestroy();
        }
    }
}