namespace GDK_TrongLe.UniCore.Extension.Unity
{
    using UnityEngine;

    public static class GameObjectExtensions
    {
        public static T OrNull<T>(this T obj) where T : Object { return obj ? obj : null; }

        public static bool IsNull<T>(this T obj) where T : Object { return obj == null; }
    }
}