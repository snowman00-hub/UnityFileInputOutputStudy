using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI ItemType;
    public TextMeshProUGUI Value;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI Describe;

    public void UpdateItemInfo(ItemInfo itemInfo)
    {
        image.sprite = Resources.Load<Sprite>(itemInfo.IconFilePath);
        ItemType.text = itemInfo.ItemType.ToString();
        Value.text = itemInfo.Value;
        Price.text = itemInfo.Price;
        Describe.text = itemInfo.Description;
    }
}
