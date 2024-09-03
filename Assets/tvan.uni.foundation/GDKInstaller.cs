namespace tvan.uni.foundation
{
    using tvan.uni.foundation.UniCore;
    using tvan.uni.foundation.UniData.Scripts.Csv.CsvController;
    using tvan.uni.foundation.UniData.Scripts.LocalData;
    using tvan.uni.foundation.UniUI.Scripts;
    using UnityEngine.EventSystems;
    using Zenject;

    public class GdkInstaller : MonoInstaller<GdkInstaller>
    {
        public override void InstallBindings()
        {
            // Bind your classes here
            UniCoreInstaller.Install(this.Container);
            UniUIInstaller.Install(this.Container);
            UniLocalDataInstaller.Install(this.Container);
            CsvDataInstaller.Install(this.Container);
            
            this.Container.Bind<EventSystem>().FromComponentInNewPrefabResource("EventSystem").AsCached().NonLazy();
        }
    }
}