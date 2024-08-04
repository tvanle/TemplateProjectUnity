namespace GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintController
{
    using DataManager.Blueprint.Signals;
    using GDK_TrongLe.UniCore.Extension.Unity;
    using GDK_TrongLe.UniCore.SignalBus;
    using GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintReader;
    using Zenject;

    /// <summary>
    ///     Binding all services of the blueprint control flow at here
    /// </summary>
    public class BlueprintServicesInstaller : Installer<BlueprintServicesInstaller>
    {
        public override void InstallBindings()
        {
            //BindBlueprint reader for mobile
            this.Container.Bind<BlueprintReaderManager>().AsCached();

            this.Container.BindAllDerivedTypes<IGenericBlueprintReader>(true);

            this.Container.DeclareSignal<LoadBlueprintDataSucceedSignal>();
            this.Container.DeclareSignal<LoadBlueprintDataProgressSignal>();
            this.Container.DeclareSignal<ReadBlueprintProgressSignal>();
        }
    }
}