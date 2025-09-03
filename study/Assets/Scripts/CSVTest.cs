using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;
using System;

public class CsvData
{
    public string Id { get;set;}
    public string String { get;set;}
}

public class CSVTest : MonoBehaviour
{
    private void Start()
    {
        var csv = Resources.Load<TextAsset>("DataTables/StringTableKr");
        using(var reader = new StringReader(csv.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<CsvData>();
            foreach (var record in records)
            {
                Debug.Log($"{record.Id} : {record.String}");
            }
        }
    }
}
