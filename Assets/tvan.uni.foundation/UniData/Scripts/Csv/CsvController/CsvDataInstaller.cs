namespace tvan.uni.foundation.UniData.Scripts.Csv.CsvController
{
    using GDK_TrongLe.UniCore.Extension.Unity;
    using tvan.uni.foundation.UniCore.SignalBus;
    using tvan.uni.foundation.UniData.Scripts.Csv.CsvReader;
    using tvan.uni.foundation.UniData.Scripts.Csv.Signal;
    using Zenject;

    public class CsvDataInstaller : Installer<CsvDataInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.Bind<CsvReaderManager>().AsCached();
            this.Container.BindAllDerivedTypes<IGenericCsvReader>(true);

            this.Container.DeclareSignal<OnLoadCsvSucceedSignal>();
        }
    }
}