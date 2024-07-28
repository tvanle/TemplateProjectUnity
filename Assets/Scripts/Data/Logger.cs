namespace Data
{
    using GDK_TrongLe.UniData.Scripts.UserData;
    using UnityEngine;
    using Zenject;

    public class Logger : MonoBehaviour
    {
        [Inject] private DiContainer             diContainer;
        [Inject] private IHandleUserDataServices handleUserDataServices;

        private void Awake()
        {
            var userLocalData = this.diContainer.Resolve(typeof(UserLocalData)) as UserLocalData;
            ;

            userLocalData.Name = "Trang Le";
            userLocalData.Id   = 15;
            this.handleUserDataServices.SaveAll();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) this.Check();
        }

        private async void Check() { }
    }
}