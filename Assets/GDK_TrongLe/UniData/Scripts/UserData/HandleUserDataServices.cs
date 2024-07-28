namespace GDK_TrongLe.UniData.Scripts.UserData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cysharp.Threading.Tasks;
    using GDK_TrongLe.UniCore.Extension;
    using GDK_TrongLe.UniData.Scripts.Interface;
    using Newtonsoft.Json;
    using UnityEngine;

    public class HandleUserDataServices : IHandleUserDataServices
    {
        public const string UserDataPrefix = "LD-";

        public static string KeyOf(Type type) { return UserDataPrefix + type.Name; }

        public static readonly JsonSerializerSettings JsonSetting = new()
        {
            TypeNameHandling      = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public Dictionary<string, ILocalData> UserDataCache { get; } = new();

        public async UniTask Save<T>(T data, bool force = false) where T : class, ILocalData
        {
            var key = KeyOf(typeof(T));

            this.UserDataCache.TryAdd(key, data);

            if (!force) return;

            await this.SaveJsons((key, JsonConvert.SerializeObject(data, JsonSetting)));
            Debug.Log($"Saved {key}");
        }

        public async UniTask<T> Load<T>() where T : class, ILocalData { return (T)(await this.Load(typeof(T)))[0]; }

        public async UniTask<ILocalData[]> Load(params Type[] types)
        {
            var keys = types.Select(KeyOf).ToArray();

            var jsons = await this.LoadJsons(keys);

            return IterTools.Zip(types, keys, jsons, (type, key, json) => this.InternalLoad(key, json, type)).ToArray();
        }

        private ILocalData InternalLoad(string key, string json, Type type)
        {
            return this.UserDataCache.GetOrAdd(key, () =>
            {
                var result = string.IsNullOrEmpty(json)
                    ? Activator.CreateInstance(type)
                    : JsonConvert.DeserializeObject(json, type, JsonSetting);

                if (result is not ILocalData data)
                {
                    Debug.LogError($"Failed to load data {key}");

                    return null;
                }

                if (string.IsNullOrEmpty(json))
                {
                    Debug.Log($"Init data {key}");
                    data.Init();
                }

                Debug.Log($"Loaded {key}");

                return data;
            });
        }

        public async UniTask SaveAll()
        {
            await this.SaveJsons(this.UserDataCache.Select(value =>
            {
                Debug.Log($"Saved {value.Key} {JsonConvert.SerializeObject(value.Value, JsonSetting)}");

                return (value.Key, JsonConvert.SerializeObject(value.Value, JsonSetting));
            }).ToArray());
            Debug.Log("Saved all data");
        }

        public UniTask SaveJsons(params (string key, string json)[] values)
        {
            values.ForEach(PlayerPrefs.SetString);
            PlayerPrefs.Save();

            return UniTask.CompletedTask;
        }

        public UniTask<string[]> LoadJsons(params string[] keys) { return UniTask.FromResult(keys.Select(PlayerPrefs.GetString).ToArray()); }
    }
}