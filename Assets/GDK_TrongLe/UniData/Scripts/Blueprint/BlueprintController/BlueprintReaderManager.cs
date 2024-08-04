namespace GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cysharp.Threading.Tasks;
    using DataManager.Blueprint.Signals;
    using GDK_TrongLe.UniCore.Extension;
    using GDK_TrongLe.UniCore.SignalBus;
    using GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintReader;
    using UnityEngine;
    using Zenject;

    /// <summary>
    ///     The main manager for reading blueprints pipeline/>.
    /// </summary>
    public class BlueprintReaderManager
    {
        #region zeject

        private readonly SignalBus   signalBus;
        private readonly DiContainer diContainer;

        #endregion

        private readonly ReadBlueprintProgressSignal readBlueprintProgressSignal = new();

        public BlueprintReaderManager(SignalBus signalBus, DiContainer diContainer)
        {
            this.signalBus   = signalBus;
            this.diContainer = diContainer;
        }

        public virtual async UniTask LoadBlueprint()
        {
            Debug.Log("[BlueprintReader] Start loading");
            this.signalBus.Fire(new LoadBlueprintDataProgressSignal { Percent = 1f });

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

            this.signalBus.Fire<LoadBlueprintDataSucceedSignal>();
        }

        private UniTask ReadAllBlueprint()
        {
            var listReadTask    = new List<UniTask>();
            var allDerivedTypes = ReflectionUtils.GetAllDerivedTypes<IGenericBlueprintReader>();
            var blueprintTypes  = allDerivedTypes as Type[] ?? allDerivedTypes.ToArray();
            this.readBlueprintProgressSignal.MaxBlueprint    = blueprintTypes.Count();
            this.readBlueprintProgressSignal.CurrentProgress = 0;
            this.signalBus.Fire(this.readBlueprintProgressSignal); // Inform that we just start reading blueprint
            foreach (var blueprintType in blueprintTypes)
            {
                var blueprintInstance = (IGenericBlueprintReader)this.diContainer.Resolve(blueprintType);
                if (blueprintInstance != null)
                    listReadTask.Add(UniTask.RunOnThreadPool(() => this.OpenReadBlueprint(blueprintInstance)));
                else
                    Debug.Log($"Can not resolve blueprint {blueprintType.Name}");
            }

            return UniTask.WhenAll(listReadTask);
        }

        private async UniTask OpenReadBlueprint(IGenericBlueprintReader blueprintReader)
        {
            var bpAttribute = blueprintReader.GetCustomAttribute<BlueprintReaderAttribute>();
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
                        result = ((TextAsset)await Resources.LoadAsync<TextAsset>("BlueprintData/" + bpAttribute.DataPath)).text;
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
                    await blueprintReader.DeserializeFromCsv(rawCsv);
                    lock (this.readBlueprintProgressSignal)
                    {
                        this.readBlueprintProgressSignal.CurrentProgress++;
                        this.signalBus.Fire(this.readBlueprintProgressSignal);
                    }
                }
                else
                {
                    Debug.LogWarning($"[BlueprintReader] Unable to load {bpAttribute.DataPath} from resource folder !!");
                }
            }
            else
            {
                Debug.LogWarning($"[BlueprintReader] Class {blueprintReader} does not have BlueprintReaderAttribute yet");
            }
        }
    }
}