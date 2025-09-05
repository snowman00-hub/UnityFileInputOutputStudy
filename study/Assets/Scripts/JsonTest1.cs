using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

class A
{

}

[Serializable]
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health}";
    }
}

public class JsonTest1 : MonoBehaviour
{
    private int number;

    public UnityEngine.Object obj;


    private void Start()
    {
        
        
        var obj = new PlayerState
        {
            playerName = "ABC",
            lives = 10,
            health = 10.999f,
        };

        var path = Path.Combine(Application.persistentDataPath, "test.json");
        string json = JsonConvert.SerializeObject(
            obj, Formatting.Indented, new Vector3Converter());
        File.WriteAllText(path, json);

        var json2 = File.ReadAllText(path);
        var obj2 = JsonConvert.DeserializeObject<PlayerState>(
            json2, new Vector3Converter());
        Debug.Log(obj2);
    }
}
