using System;
using System.Collections.Generic;

namespace Game.Utils
{
    public static class CollectionExtension
    {
        public static void Foreach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T element in collection) action.Invoke(element);
        }
        
        public static void Foreach<T>(this IReadOnlyList<T> collection, Action<T> action)
        {
            for (int i = collection.Count - 1; i >= 0; i--) action.Invoke(collection[i]);
        }
    }
}