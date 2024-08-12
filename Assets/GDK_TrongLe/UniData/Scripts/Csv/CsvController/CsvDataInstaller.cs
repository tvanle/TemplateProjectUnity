namespace GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintController
{
    using GDK_TrongLe.UniCore.Extension.Unity;
    using GDK_TrongLe.UniData.Scripts.Csv.CsvReader;
    using Zenject;

    public class CsvDataInstaller : Installer<CsvDataInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.Bind<CsvReaderManager>().AsCached();
            this.Container.BindAllDerivedTypes<IGenericCsvReader>(true);
        }
    }
}