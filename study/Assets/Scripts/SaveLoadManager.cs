using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using SaveDataVC = SaveDataV4;

public class SaveLoadManager
{
    public static int SaveDataVersion { get; } = 4;

    public static SaveDataVC Data { get; set;} = new SaveDataVC();

    private static readonly string[] SaveFilename =
    {
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json",
    };

    public static string SaveDirectory => $"{Application.persistentDataPath}/Save";

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    public static bool Save(int slot = 0)
    {
        if(Data == null || slot < 0 || slot > SaveFilename.Length)
            return false;

        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            var path = Path.Combine(SaveDirectory, SaveFilename[slot]);
            var json = JsonConvert.SerializeObject(Data, settings);
            File.WriteAllText(path, json);

            // 바이트 배열로 바꾸고 압축하고
            // 암호화(s 알고리즘, 블록 알고리즘) 후에
            // 바이너리 모드로 쓰기

            return true;
        }
        catch
        {
            Debug.Log("Save 예외 발생");
            return false;
        }
    }

    public static bool Load(int slot = 0)
    {
        if (slot < 0 || slot > SaveFilename.Length)
            return false;

        var path = Path.Combine(SaveDirectory, SaveFilename[slot]);
        if(!File.Exists(path))
            return false;

        try
        {
            var json = File.ReadAllText(path);
            var dataSave = JsonConvert.DeserializeObject<SaveData>(json, settings);
            while(dataSave.Version < SaveDataVersion)
            {
                dataSave = dataSave.VersionUp();
            }
            Data = dataSave as SaveDataVC;
            return true;
        }
        catch
        {
            Debug.Log("Load 예외 발생");
            return false;
        }       
    }
}
