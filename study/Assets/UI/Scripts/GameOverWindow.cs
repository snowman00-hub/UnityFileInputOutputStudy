using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{
    public Button nextButton;
    public List<TextMeshProUGUI> statTexts;
    public TextMeshProUGUI totalScore;
    public float writeTextInterval = 0.2f;

    private Coroutine coroutine;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnClickNextButton);
    }

    public override void Open()
    {
        foreach (var text in statTexts)
        {
            text.text = string.Empty;
        }
        totalScore.text = string.Empty;
        firstSelected = nextButton.gameObject;

        base.Open();

        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(CoWriteStats());
    }

    public void OnClickNextButton()
    {
        manager.Open(Windows.KeyBoard);
    }

    public IEnumerator CoWriteStats()
    {
        int i = 1;
        foreach (var text in statTexts)
        {
            string stat = $"{Random.Range(0, 9999):D4}".PadLeft(10);
            text.text = $"Stat {i++} {stat}";
            yield return new WaitForSeconds(writeTextInterval);
        }

        totalScore.text = $"{Random.Range(0, int.MaxValue):D10}";

        coroutine = null;
    }
}
