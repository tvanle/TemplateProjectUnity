namespace tvan.uni.foundation.UniCore.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public static class ListExtension
    {
        public static T PopFirst<T>(this IList<T> t)
        {
            T element = t[0];
            t.RemoveAt(0);

            return element;
        }

        public static void PushFirst<T>(this IList<T> t, T element) { t.Insert(0, element); }

        public static T PopLast<T>(this IList<T> t)
        {
            T element = t[t.Count - 1];
            t.RemoveAt(t.Count - 1);

            return element;
        }

        public static void PushLast<T>(this IList<T> t, T element) { t.Add(element); }

        public static T PickRandom<T>(this IEnumerable<T> ie)
        {
            List<T> t = ie as List<T> ?? ie.ToList();
            if (t.Count == 0)
            {
                Debug.LogError("Range is zero!");

                return default(T);
            }

            return t[Random.Range(0, t.Count)];
        }

        public static bool TryAdd<T>(this IList<T> t, T element)
        {
            if(t.Contains(element))
            {
                return false;
            }
            t.Add(element);

            return true;
        }

        public static T TryAdd<T, TU>(this IList<T> t, Func<T> valueFunc)
        {
            TryAdd(t, valueFunc());

            return t.Last();
        }
    }
}