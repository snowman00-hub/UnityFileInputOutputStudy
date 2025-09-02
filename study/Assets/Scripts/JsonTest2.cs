using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public List<SaveObjData> datas = new List<SaveObjData>();
}

[Serializable]
public class SaveObjData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Color color;
    public string prefabName;
}

public class JsonTest2 : MonoBehaviour
{    
    public static readonly string fileName = "cubes.json";
    public static string FileFullPath => Path.Combine(Application.persistentDataPath, fileName);

    public GameObject[] prefab;

    public void Save()
    {
        var gameobjects = GameObject.FindGameObjectsWithTag(Tag.Save);
        SaveData saveData = new SaveData();
        foreach (var go in gameobjects)
        {
            SaveObjData saveObjData = new SaveObjData();
            saveObjData.position = go.transform.position;
            saveObjData.rotation = go.transform.rotation;
            saveObjData.scale = go.transform.localScale;
            saveObjData.color = go.GetComponent<Renderer>().material.color;
            saveObjData.prefabName = go.name.Replace("(Clone)", "");
            saveData.datas.Add(saveObjData);
        }

        JsonConverter[] converters = new JsonConverter[]
        {
            new Vector3Converter(),
            new QuaternionConverter(),
            new ColorConverter(),
        };

        var json = JsonConvert.SerializeObject(saveData,
            Formatting.Indented ,converters);
        File.WriteAllText(FileFullPath, json);
    }

    public void Load()
    {
        var json = File.ReadAllText(FileFullPath);

        JsonConverter[] converters = new JsonConverter[]
        {
            new Vector3Converter(),
            new QuaternionConverter(),
            new ColorConverter(),
        };

        var saveDatas = JsonConvert.DeserializeObject<SaveData>(json, converters);
        foreach (var data in saveDatas.datas)
        {
            GameObject go = null;
            for(int i = 0; i < prefab.Length; i++)
            {
                if(prefab[i].name == data.prefabName)
                {
                    go = Instantiate(prefab[i]);
                    break;
                }
            }
            go.transform.position = data.position;
            go.transform.rotation = data.rotation;
            go.transform.localScale = data.scale;
            go.GetComponent<Renderer>().material.color = data.color;
        }
    }

    public void Create()
    {
        int index = UnityEngine.Random.Range(0, prefab.Length);
        var go = Instantiate(prefab[index]);
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-10f, 10f),
            UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
        go.transform.position = pos;

        Quaternion rot = Quaternion.Euler(UnityEngine.Random.Range(0f, 360f),
            UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
        go.transform.rotation = rot;

        Vector3 scale = new Vector3(UnityEngine.Random.Range(0.1f, 5f),
            UnityEngine.Random.Range(0.1f, 5f), UnityEngine.Random.Range(0.1f, 5f));
        go.transform.localScale = scale;

        Color randomColor = new Color(
            UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)); 
        go.GetComponent<Renderer>().material.color = randomColor;
    }
}
