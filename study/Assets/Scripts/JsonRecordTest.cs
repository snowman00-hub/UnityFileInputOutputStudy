using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

[Serializable]
public class RecordDatas
{
    public List<RecordObjData> datas = new List<RecordObjData>();
}

[Serializable]
public class RecordObjData
{
    public Vector3 position;
}

public class JsonRecordTest : MonoBehaviour
{
    public static readonly string fileName = "record.json";
    public static string FullFilePath =>
        Path.Combine(Application.persistentDataPath, fileName);

    public GameObject target;
    public GameObject recordMark;
    public float recordInterval = 0.033f;

    private float lastRecordTime;
    private bool isRecording = false;
    private bool isPlaying = false;

    private RecordDatas writeRecords = new RecordDatas();
    private RecordDatas readRecords = new RecordDatas();
    private int readIndex = 0;

    private void Update()
    {
        if (isRecording && lastRecordTime + recordInterval < Time.time)
        {
            lastRecordTime = Time.time;
            var data = new RecordObjData();
            data.position = target.transform.position;
            writeRecords.datas.Add(data);
        }

        if (isPlaying && lastRecordTime + recordInterval < Time.time)
        {
            lastRecordTime = Time.time;

            if (readIndex >= readRecords.datas.Count)
                return;

            target.transform.position = readRecords.datas[readIndex++].position;
        }
    }

    public void Record()
    {
        if (isPlaying)
            return;

        if (!isRecording)
        {
            isRecording = true;
            recordMark.SetActive(true);
            File.Delete(FullFilePath);
            writeRecords.datas.Clear();
        }
        else
        {
            isRecording = false;
            recordMark.SetActive(false);

            var converters = new JsonConverter[] 
            {
                new Vector3Converter(),
            };

            var json = JsonConvert.SerializeObject(writeRecords,
                Formatting.Indented, converters);

            File.WriteAllText(FullFilePath, json);
        }
    }

    public void Play()
    {
        if (isRecording)
            return;

        if (!isPlaying)
        {
            isPlaying = true;
            readIndex = 0;
            lastRecordTime = Time.time;
            var json = File.ReadAllText(FullFilePath);

            JsonConverter[] converters = new JsonConverter[]
            {
                new Vector3Converter(),
            };
            readRecords = JsonConvert.DeserializeObject<RecordDatas>(json, converters);
        }
        else
        {
            isPlaying = false;
            if (writeRecords.datas.Count != 0)
                target.transform.position = writeRecords.datas[0].position;
        }
    }
}
