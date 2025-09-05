using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

[SerializeField]
public class SomeClass
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
}


public class JsonTest2 : MonoBehaviour
{
    public static readonly string fileName = "cube.json";
    public static string FileFullPath => Path.Combine(Application.persistentDataPath, fileName);

    public GameObject target;

    public void Save()
    {
        SomeClass obj = new SomeClass();
        obj.pos = target.transform.position;
        obj.rot = target.transform.rotation;

        var json = JsonConvert.SerializeObject(
            target.transform.position, new Vector3Converter());
        File.WriteAllText(FileFullPath, json);
    }

    public void Load()
    {
        var json = File.ReadAllText(FileFullPath);
        var position = JsonConvert.DeserializeObject<Vector3>(
            json, new Vector3Converter());
        target.transform.position = position;
    }
}
