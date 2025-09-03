using UnityEngine;
using TMPro;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizationTest : MonoBehaviour
{
    public string stringId;

#if UNITY_EDITOR
    public Languages editorLang;
#endif

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        if(Application.isPlaying)
        {
            OnChangeLanguage();
        }
        else
        {
            OnChangeLanguage(editorLang);
        }
#else
        OnChangeLanguage();
#endif
    }

    public void OnChangeLanguage()
    {
        var stringTable = DataTableManager.StringTable;
        text.text = stringTable.Get(stringId);
    }

#if UNITY_EDITOR
    public void OnChangeLanguage(Languages lang)
    {
        var tableId = DataTableIds.StringTableIds[(int)lang];
        var stringTable = DataTableManager.Get<StringTable>(tableId);
        text.text = stringTable.Get(stringId);
    }
#endif
}
