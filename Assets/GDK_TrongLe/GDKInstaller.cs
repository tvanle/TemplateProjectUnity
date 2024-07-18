namespace Combine.Installer
{
    using UnityEngine.EventSystems;

    public class GDKInstaller : MonoInstaller<GDKInstaller>
    {
        public override void InstallBindings()
        {
            // Bind your classes here
            UniCoreInstaller.Install(this.Container);
            UniCoreInstaller.Install(this.Container);
            UniUIInstaller.Install(this.Container);

            this.Container.Bind<EventSystem>().FromComponentInNewPrefabResource("EventSystem").AsCached().NonLazy();
        }
    }
}