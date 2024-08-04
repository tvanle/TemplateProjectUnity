namespace GDK_TrongLe
{
    using GDK_TrongLe.UniCore;
    using GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintController;
    using GDK_TrongLe.UniData.Scripts.LocalData;
    using GDK_TrongLe.UniUI.Scripts;
    using UnityEngine.EventSystems;
    using Zenject;

    public class GDKInstaller : MonoInstaller<GDKInstaller>
    {
        public override void InstallBindings()
        {
            // Bind your classes here
            UniCoreInstaller.Install(this.Container);
            UniUIInstaller.Install(this.Container);
            UniLocalDataInstaller.Install(this.Container);
            BlueprintServicesInstaller.Install(this.Container);
            
            this.Container.Bind<EventSystem>().FromComponentInNewPrefabResource("EventSystem").AsCached().NonLazy();
        }
    }
}