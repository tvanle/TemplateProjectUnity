namespace Data
{
    using System;
    using System.IO;
    using Data.Csv;
    using Sylvan.Data.Csv;
    using Tvan.Foundation.DI;
    using Tvan.Foundation.UniCore.Extension;
    using UnityEngine;
    using VContainer;

    public class Logger : MonoBehaviour
    {
        [Inject] private IDependencyContainer diContainer;
        [Inject] private LevelCsv             levelCsv;

        private void Awake() { Debug.Log(DateTime.UtcNow); }

        private async void Start()
        {
            // T?i t?p CSV t? Resources
            var csvFile = Resources.Load<TextAsset>("CsvData/Level"); // "data" kh�ng c?n ph?n m? r?ng .csv
            Debug.Log(csvFile.text);
            if (csvFile != null)
                // T?o m?t MemoryStream t? n?i dung c?a TextAsset
                using (var stream = new MemoryStream(csvFile.bytes))
                using (var reader = new StreamReader(stream))
                {
                    // T?o d?i tu?ng CsvDataReader t? n?i dung c?a StreamReader
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
                Debug.LogError("Kh�ng t�m th?y t?p CSV trong Resources.");
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