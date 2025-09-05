using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiItemInfo : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI itemValue;
    public TextMeshProUGUI itemCost;

    public void SetEmpty()
    {
        icon.sprite = null;
        itemName.text = string.Empty;
        itemDescription.text = string.Empty;
        itemType.text = string.Empty;
        itemValue.text = string.Empty;
        itemCost.text = string.Empty;
    }

    public void SetItem(SaveItemData data)
    {
        icon.sprite = data.itemData.SpriteIcon;
        itemName.text = data.itemData.StringName;
        itemDescription.text = data.itemData.StringDesc;
        itemType.text = data.itemData.Type.ToString();
        itemValue.text = data.itemData.Value.ToString();
        itemCost.text = data.itemData.Cost.ToString();
    }
}
