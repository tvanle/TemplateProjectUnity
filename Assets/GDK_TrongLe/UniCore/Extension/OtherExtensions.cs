namespace GDK_TrongLe.UniCore.Extension
{
    using Newtonsoft.Json;
    using UnityEngine;

    public static class OtherExtensions
    {
        public static string ToJson<T>(this T obj) { return JsonConvert.SerializeObject(obj); }

        public static string GetPath(this Transform current)
        {
            if (current.parent == null)
                return current.name;

            return current.parent.GetPath() + "/" + current.name;
        }

        public static string Path(this Component component) { return GetPath(component.transform); }

        public static string Path(this GameObject gameObject) { return GetPath(gameObject.transform); }
    }
}