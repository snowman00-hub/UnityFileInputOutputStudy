using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiInvenSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,
        CostAscending,
        CostDescending,
    }

    public enum FilteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable,
    }

    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs,rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs,rhs) => rhs.creationTime.CompareTo(lhs.creationTime),
        (lhs,rhs) => lhs.itemData.StringName.CompareTo(rhs.itemData.StringName),
        (lhs,rhs) => rhs.itemData.StringName.CompareTo(lhs.itemData.StringName),
        (lhs,rhs) => lhs.itemData.Cost.CompareTo(rhs.itemData.Cost),
        (lhs,rhs) => rhs.itemData.Cost.CompareTo(lhs.itemData.Cost),
    };

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        x => true,
        x => x.itemData.Type == ItemTypes.Weapon,
        x => x.itemData.Type == ItemTypes.Equip,
        x => x.itemData.Type == ItemTypes.Consumable,
    };

    public UiInvenSlot prefab;

    public ScrollRect scrollRect;

    private List<UiInvenSlot> slotList = new List<UiInvenSlot>();

    public int maxCount = 30;

    private List<SaveItemData> currentItemList = new List<SaveItemData>();

    private SortingOptions sorting = SortingOptions.NameAscending;
    private FilteringOptions filtering = FilteringOptions.None;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            sorting = value;
            UpdateSlots(currentItemList);
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            filtering = value;
            UpdateSlots(currentItemList);
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;

    public void Save()
    {
        SaveLoadManager.Data.saveItemList = currentItemList;
        SaveLoadManager.Save();
    }

    public void Load()
    {
        if (SaveLoadManager.Load())
        {
            currentItemList = SaveLoadManager.Data.saveItemList;
        }
        UpdateSlots(currentItemList);
        onUpdateSlots.Invoke();
    }

    private void OnEnable()
    {
        Load();
    }

    private void OnDisable()
    {
        Save();
    }

    private void UpdateSlots(List<SaveItemData> itemList)
    {
        var list = itemList.Where(filterings[(int)Filtering]).ToList();
        list.Sort(comparisons[(int)Sorting]);

        if (slotList.Count < list.Count)
        {
            for (int i = slotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                var button = newSlot.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.ItemData);
                });
                slotList.Add(newSlot);
            }
        }

        for (int i = 0; i < slotList.Count; i++)
        {
            if (i < list.Count)
            {
                slotList[i].SetItem(list[i]);
                slotList[i].gameObject.SetActive(true);
            }
            else
            {
                slotList[i].SetEmpty();
                slotList[i].gameObject.SetActive(false);
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    public void AddRandomItem()
    {
        var itemInstance = new SaveItemData();
        itemInstance.itemData = DataTableManger.ItemTable.GetRandom();

        currentItemList.Add(itemInstance);
        UpdateSlots(currentItemList);
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
            return;

        currentItemList.Remove(slotList[selectedSlotIndex].ItemData);
        UpdateSlots(currentItemList);
    }
}
