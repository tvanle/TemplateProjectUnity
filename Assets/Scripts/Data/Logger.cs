namespace Data
{
    using System;
    using System.IO;
    using Data.Blueprint;
    using GDK_TrongLe.UniCore.Extension;
    using Sylvan.Data.Csv;
    using UnityEngine;
    using Zenject;

    public class Logger : MonoBehaviour
    {
        [Inject] private DiContainer diContainer;
        [Inject] private LevelCsv    levelCsv;

        private void Awake() { Debug.Log(DateTime.UtcNow); }

        private async void Start()
        {
            // Tải tệp CSV từ Resources
            var csvFile = Resources.Load<TextAsset>("CsvData/Level"); // "data" không cần phần mở rộng .csv
            Debug.Log(csvFile.text);
            if (csvFile != null)
                // Tạo một MemoryStream từ nội dung của TextAsset
                using (var stream = new MemoryStream(csvFile.bytes))
                using (var reader = new StreamReader(stream))
                {
                    // Tạo đối tượng CsvDataReader từ nội dung của StreamReader
                    var opts = new CsvDataReaderOptions();
                    using (var csv = await CsvDataReader.CreateAsync(reader, opts))
                    {
                        var i = 1;
                        while (csv.Read())
                        {
                            Debug.Log(i);
                            Debug.Log(csv);
                            i++;
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
            Debug.Log(this.levelCsv.Values.Count);
            this.levelCsv.Values.ForEach(x => { Debug.Log($"{x.GameType} {x.BoolBlueprint.Count} {x.BoolBlueprint.GetDataById(1)}"); });
        }
    }
}