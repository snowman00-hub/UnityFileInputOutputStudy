using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image itemImage;
    public ItemInfo itemInfo;
    public Button button;

    private ItemInfoPanel panel;

    private void Start()
    {
        panel = GameObject.FindWithTag(Tag.ItemInfoPanel).GetComponent<ItemInfoPanel>();
        button.onClick.AddListener(() => panel.UpdateItemInfo(itemInfo));
    }
}
