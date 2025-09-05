using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiInvenSlot : MonoBehaviour
{
    public int slotIndex { get; set; }

    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveItemData ItemData { get; private set; }

    public void SetEmpty()
    {
        ItemData = null;
        imageIcon.sprite = null;
        textName.text = string.Empty;
    }

    public void SetItem(SaveItemData data)
    {
        ItemData = data;
        imageIcon.sprite = data.itemData.SpriteIcon;
        textName.text = data.itemData.StringName;
    }
}
