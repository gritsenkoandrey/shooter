﻿using System;

namespace Game.Core.Implementation
{
    public static class EntityBridge<T> where T : Entity
    {
        public static event Action<T> OnRegistered;
        public static event Action<T> OnUnregistered;

        public static void Registered(Entity entity)
        {
            if (entity is T component)
            {
                OnRegistered?.Invoke(component);
            }
        }

        public static void Unregistered(Entity entity)
        {
            if (entity is T component)
            {
                OnUnregistered?.Invoke(component);
            }
        }
    }
}