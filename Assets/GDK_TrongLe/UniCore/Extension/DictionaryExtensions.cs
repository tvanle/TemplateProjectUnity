namespace GDK_TrongLe.UniCore.Extension
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class DictionaryExtensions
    {
        public static TV GetOrAdd<TK, TV>(this IDictionary<TK, TV> dictionary, TK key, Func<TV> valueFunc)
        {
            if (dictionary.TryGetValue(key, out var val)) return val;
            dictionary.Add(key, valueFunc());

            return dictionary[key];
        }

        public static TVal GetDataById<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey id)
        {
            if (dictionary.TryGetValue(id, out var result))
                return result;

            throw new InvalidDataException($"Dictionary {dictionary.GetType().Name} doesn't contain Id {id}");
        }
    }
}