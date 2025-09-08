using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardWindow : GenericWindow
{
    public TextMeshProUGUI writingText;
    public TextMeshProUGUI caret;

    public Button cancelButton;
    public Button deleteButton;
    public Button acceptButton;
    public List<Button> keys;

    private Coroutine coroutine;
    private StringBuilder sb = new StringBuilder();
    private const int maxWritingSize = 7;

    private void Awake()
    {
        cancelButton.onClick.AddListener(OnClickCancel);
        deleteButton.onClick.AddListener(OnClickDelete);
        acceptButton.onClick.AddListener(OnClickAccept);
        foreach (var key in keys)
        {
            TextMeshProUGUI tmp = key.GetComponentInChildren<TextMeshProUGUI>();
            key.onClick.AddListener(() => OnClickKey(tmp.text));
        }
    }

    public void OnClickKey(string text)
    {
        if (writingText.text.Length == maxWritingSize)
            return;

        sb.Append(text);
        writingText.text = sb.ToString();
    }

    public override void Open()
    {
        writingText.text = string.Empty;
        sb.Clear();

        base.Open();

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(CoCaretEffect());
    }

    public IEnumerator CoCaretEffect()
    {
        while (true)
        {
            bool isActive = caret.gameObject.activeSelf;
            bool CanWrite = true;

            if (writingText.text.Length == maxWritingSize)
                CanWrite = false;

            caret.gameObject.SetActive(!isActive && CanWrite);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnClickCancel()
    {
        writingText.text = string.Empty;
        sb.Clear();
    }

    public void OnClickDelete()
    {
        if (writingText.text.Length == 0)
            return;

        sb.Remove(sb.Length - 1, 1);
        writingText.text = sb.ToString();
    }

    public void OnClickAccept()
    {
        manager.Open(Windows.Difficulty);
    }
}
