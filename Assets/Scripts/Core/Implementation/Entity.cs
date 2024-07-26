using UnityEngine;

namespace Game.Core.Implementation
{
    public abstract class Entity : MonoBehaviour
    {
        protected virtual void OnEntityCreate() { }
        protected virtual void OnEntityEnable() { }
        protected virtual void OnEntityDisable() { }
        protected virtual void OnEntityDestroy() { }
    }
}