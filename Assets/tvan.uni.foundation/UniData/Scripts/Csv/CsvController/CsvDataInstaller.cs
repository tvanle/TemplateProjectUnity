namespace tvan.uni.foundation.UniData.Scripts.Csv.CsvController
{
    using GDK_TrongLe.UniCore.Extension.Unity;
    using tvan.uni.foundation.UniData.Scripts.Csv.CsvReader;
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