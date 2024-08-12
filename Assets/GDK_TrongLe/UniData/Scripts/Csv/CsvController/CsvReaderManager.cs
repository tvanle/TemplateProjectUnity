namespace GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cysharp.Threading.Tasks;
    using GDK_TrongLe.UniCore.Extension;
    using GDK_TrongLe.UniCore.SignalBus;
    using GDK_TrongLe.UniData.Scripts.Csv.CsvReader;
    using UnityEngine;
    using Zenject;

    /// <summary>
    ///     The main manager for reading blueprints pipeline/>.
    /// </summary>
    public class CsvReaderManager
    {
        #region zeject

        private readonly SignalBus   signalBus;
        private readonly DiContainer diContainer;

        #endregion

        public CsvReaderManager(SignalBus signalBus, DiContainer diContainer)
        {
            this.signalBus   = signalBus;
            this.diContainer = diContainer;
        }

        public virtual async UniTask LoadCsvData()
        {
            Debug.Log("[BlueprintReader] Start loading");
            //Load all blueprints to instances
            try
            {
                await this.ReadAllBlueprint();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            Debug.Log("[BlueprintReader] All blueprint are loaded");
        }

        private UniTask ReadAllBlueprint()
        {
            var listReadTask    = new List<UniTask>();
            var allDerivedTypes = ReflectionUtils.GetAllDerivedTypes<IGenericCsvReader>();
            var blueprintTypes  = allDerivedTypes as Type[] ?? allDerivedTypes.ToArray();
            foreach (var blueprintType in blueprintTypes)
            {
                var blueprintInstance = (IGenericCsvReader)this.diContainer.Resolve(blueprintType);
                if (blueprintInstance != null)
                    listReadTask.Add(UniTask.RunOnThreadPool(() => this.OpenReadBlueprint(blueprintInstance)));
                else
                    Debug.Log($"Can not resolve blueprint {blueprintType.Name}");
            }

            return UniTask.WhenAll(listReadTask);
        }

        private async UniTask OpenReadBlueprint(IGenericCsvReader csvReader)
        {
            var bpAttribute = csvReader.GetCustomAttribute<CsvInfoAttribute>();
            if (bpAttribute != null)
            {
                // Try to load a raw blueprint file from local or resource folder
                var rawCsv = await LoadRawCsvFromResourceFolder();

                async UniTask<string> LoadRawCsvFromResourceFolder()
                {
                    await UniTask.SwitchToMainThread();
                    var result = string.Empty;
                    try
                    {
                        result = ((TextAsset)await Resources.LoadAsync<TextAsset>("CsvData/" + bpAttribute.DataPath)).text;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Load {bpAttribute.DataPath} blueprint error!!!");
                        Debug.LogException(e);
                    }
                    
                    await UniTask.SwitchToThreadPool();
                    return result;
                }

                // Deserialize the raw blueprint to the blueprint reader instance

                if (!string.IsNullOrEmpty(rawCsv))
                {
                    await csvReader.DeserializeFromCsv(rawCsv);
                }
                else
                {
                    Debug.LogError($"[BlueprintReader] Unable to load {bpAttribute.DataPath} from resource folder !!");
                }
            }
            else
            {
                Debug.LogError($"[BlueprintReader] Class {csvReader} does not have BlueprintReaderAttribute yet");
            }
        }
    }
}