using UnityEngine;
using TMPro;

public class StringTableTest : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro.text = DataTableManager.StringTable.Get(id);
    }
}
