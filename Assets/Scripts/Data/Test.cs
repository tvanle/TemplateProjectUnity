namespace Data
{
    using GDK_TrongLe.UniData.Scripts.UserData;
    using UnityEngine;
    using Zenject;

    public class Test : MonoBehaviour
    {
        [Inject] private UserLocalData           userLocalData;
        [Inject] private IHandleUserDataServices handleUserDataServices;

        private void Awake()
        {
            var x = this.userLocalData.Id;
            var y = this.userLocalData.Name;
            Debug.Log($"Id: {x}, Name: {y}");
        }
    }
}