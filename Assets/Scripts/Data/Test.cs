namespace Data
{
    using Cysharp.Threading.Tasks;
    using GDK_TrongLe.UniData.Scripts.UserData;
    using Unity.Plastic.Newtonsoft.Json;
    using UnityEngine;
    using Zenject;

    public class Test : MonoBehaviour
    {
        [Inject] private DiContainer             diContainer;
        [Inject] private IHandleUserDataServices handleUserDataServices;

        private async void Awake()
        {
            var userLocalData = this.diContainer.Resolve<UserLocalData>();
            await UniTask.Delay(1000);
            Debug.Log(JsonConvert.SerializeObject(userLocalData));
            var tmp = this.handleUserDataServices as HandleUserDataServices;
            Debug.Log(tmp.UserDataCache);
        }
    }
}