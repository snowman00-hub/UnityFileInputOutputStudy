using UnityEngine;

public class SaveLoadTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataV1();
            SaveLoadManager.Data.PlayerName = "TEST";
            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SaveLoadManager.Load();

            Debug.Log(SaveLoadManager.Data.PlayerName);
        }
    }
}
