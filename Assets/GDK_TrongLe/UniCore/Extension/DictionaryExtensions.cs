namespace GDK_TrongLe.UniCore.Extension
{
    using System;
    using System.Collections.Generic;

    public static class DictionaryExtensions
    {
        public static TV GetOrAdd<TK, TV>(this IDictionary<TK, TV> dictionary, TK key, Func<TV> valueFunc)
        {
            if (dictionary.TryGetValue(key, out var val)) return val;
            dictionary.Add(key, valueFunc());
            return dictionary[key];
        }
    }
}