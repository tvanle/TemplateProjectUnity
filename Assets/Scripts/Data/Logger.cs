namespace Data
{
    using System.IO;
    using Data.Blueprint;
    using GDK_TrongLe.UniCore.Extension;
    using Sylvan.Data.Csv;
    using UnityEngine;
    using Zenject;

    public class Logger : MonoBehaviour
    {
        [Inject] private DiContainer    diContainer;
        [Inject] private LevelBlueprint levelBlueprint;

        private void Start()
        {
            // Tải tệp CSV từ Resources
            var csvFile = Resources.Load<TextAsset>("BlueprintData/Level"); // "data" không cần phần mở rộng .csv
            Debug.Log(csvFile.text);
            if (csvFile != null)
                // Tạo một MemoryStream từ nội dung của TextAsset
                using (var stream = new MemoryStream(csvFile.bytes))
                using (var reader = new StreamReader(stream))
                {
                    // Tạo đối tượng CsvDataReader từ nội dung của StreamReader
                    var opts = new CsvDataReaderOptions();
                    using (var csv = CsvDataReader.Create(reader, opts))
                    {
                        while (csv.Read())
                        {
                            // Debug.Log(csv);
                        }
                    }
                }
            else
                Debug.LogError("Không tìm thấy tệp CSV trong Resources.");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) this.Check();
        }

        private async void Check()
        {
            Debug.Log(this.levelBlueprint.GetDataById(1).ID);
            this.levelBlueprint.Values.ForEach(x => { Debug.Log(x.ID + " " + x.GameType + " " + x.GameTypeLevelId); });
        }
    }
}