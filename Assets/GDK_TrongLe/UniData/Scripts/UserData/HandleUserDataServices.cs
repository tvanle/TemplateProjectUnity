namespace GDK_TrongLe.UniData.Scripts.UserData
{
    using System.Linq;
    using Cysharp.Threading.Tasks;
    using GDK_TrongLe.UniCore.Extension;
    using UnityEngine;

    public class HandleLocalUserDataServices : BaseHandleUserDataServices
    {
        protected override UniTask SaveJsons(params (string key, string json)[] values)
        {
            values.ForEach(PlayerPrefs.SetString);
            PlayerPrefs.Save();

            return UniTask.CompletedTask;
        }

        protected override UniTask<string[]> LoadJsons(params string[] keys) { return UniTask.FromResult(keys.Select(PlayerPrefs.GetString).ToArray()); }
    }
}