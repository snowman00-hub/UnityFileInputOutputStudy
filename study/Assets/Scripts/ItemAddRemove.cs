using Mono.Cecil;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddRemove : MonoBehaviour
{
    private Dictionary<int, string> itemIdDictionary = new Dictionary<int, string>();

    [SerializeField]
    private GameObject Inventory;
    [SerializeField]
    private GameObject SlotPrefab;

    private Queue<GameObject> slots = new Queue<GameObject>();

    private void Start()
    {
        var itemTable = DataTableManager.ItemTable;
        var itemAllIds = itemTable.GetAllIds();
        for(int i = 0; i < itemAllIds.Count; i++)
        {
            itemIdDictionary.Add(i, itemAllIds[i]);
        }
    }

    public void AddItem()
    {
        if (slots.Count >= 15)
            return;
        
        int itemCount = itemIdDictionary.Count;
        if (itemCount == 0)
            return;

        int randomCount = Random.Range(0,itemCount);

        var itemTable = DataTableManager.ItemTable;
        var itemInfo = itemTable.Get(itemIdDictionary[randomCount]);

        var slot = Instantiate(SlotPrefab, Inventory.transform);
        var itemSlot = slot.GetComponent<ItemSlot>();
        itemSlot.itemInfo = itemInfo;
        itemSlot.itemImage.sprite = Resources.Load<Sprite>(itemInfo.IconFilePath);
        slots.Enqueue(slot);
    }

    public void RemoveItem()
    {
        if (slots.Count == 0)
            return;

        var slot = slots.Dequeue();
        Destroy(slot);
    }
}
