namespace GDK_TrongLe.UniCore.Extension
{
    using System;
    using System.Collections.Generic;

    public static class DictionaryExtensions
    {
        public static T GetOrAdd<T, TK>(this IDictionary<TK, T> dictionary, TK key, Func<T> valueFunc)
        {
            if (dictionary.TryGetValue(key, out var val)) return val;
            dictionary.Add(key, valueFunc());
            return dictionary[key];
        }
    }
}