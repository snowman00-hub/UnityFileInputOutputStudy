using UnityEngine;
using UnityEngine.UI;

public class DifficultiyWindow : GenericWindow
{
    public Difficulty difficulty = Difficulty.Normal;

    public Button previousButton;
    public ToggleGroup toggleGroup;
    public Toggle[] toggles;

    private void Awake()
    {
        previousButton.onClick.AddListener(OnClickPrevious);
    }

    public void OnClickPrevious()
    {
        SaveLoadManager.Data.Difficulty = difficulty;
        SaveLoadManager.Save();

        manager.Open(Windows.Start);
    }

    public override void Open()
    {
        difficulty = SaveLoadManager.Data.Difficulty;

        base.Open();
        toggles[(int)difficulty].isOn = true;
    }

    public void OnToggle()
    {
        for(int i=0;i<toggles.Length;i++)
        {
            if(toggles[i].isOn)
            {
                Debug.Log(i);
                break;
            }
        }
    }

    public void OnClickEasy(bool value)
    {
        if (value)
        {
            difficulty = Difficulty.Easy;
        }
    }

    public void OnClickNormal(bool value)
    {
        if (value)
        {
            difficulty = Difficulty.Normal;
        }
    }

    public void OnClickHard(bool value)
    {
        if (value)
        {
            difficulty = Difficulty.Hard;
        }
    }
}
