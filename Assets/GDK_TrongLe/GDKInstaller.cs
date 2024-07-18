namespace GDK_TrongLe
{
    using GDK_TrongLe.UniCore;
    using GDK_TrongLe.UniData.Scripts;
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
            UniDataInstaller.Install(this.Container);
            this.Container.Bind<EventSystem>().FromComponentInNewPrefabResource("EventSystem").AsCached().NonLazy();
        }
    }
}