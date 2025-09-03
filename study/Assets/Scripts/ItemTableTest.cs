using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTableTest : MonoBehaviour
{
    public string id;
    public Image image;
    public TextMeshProUGUI text;

    private void Start()
    {
        var itemInfo = DataTableManager.ItemTable.Get(id);

        image.sprite = Resources.Load<Sprite>(itemInfo.IconFilePath);

        text.text = $"{itemInfo.IconFilePath}\n" +
            $"{itemInfo.ItemType}\n" +
            $"{itemInfo.Value}\n" +
            $"{itemInfo.Price}\n" +
            $"{itemInfo.Description}";
    }
}
