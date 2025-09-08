using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{
    public Button nextButton;
    public TextMeshProUGUI leftStatsText;
    public TextMeshProUGUI rightStatsText;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnClickNextButton);
    }

    public override void Open()
    {
        leftStatsText.text = string.Empty;
        rightStatsText.text = string.Empty;
        firstSelected = nextButton.gameObject;
        base.Open();
    }

    public void OnClickNextButton()
    {
        manager.Open(Windows.Start);
    }
}
