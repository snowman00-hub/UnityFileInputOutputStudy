using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTableTest : MonoBehaviour
{
    public string id;

    public Image image;
    public TextMeshProUGUI ItemType;
    public TextMeshProUGUI Value;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI Describe;

    private void Start()
    {
        var itemInfo = DataTableManager.ItemTable.Get(id);

        image.sprite = Resources.Load<Sprite>(itemInfo.IconFilePath);
        ItemType.text = itemInfo.ItemType.ToString();
        Value.text = itemInfo.Value;
        Price.text = itemInfo.Price;
        Describe.text = itemInfo.Description;
    }
}
