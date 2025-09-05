using UnityEngine;
using TMPro;

public class UiInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiInvenSlotList slotList;

    public void OnEnable()
    {
        OnChangeSorting(sorting.value);
        OnChangeFiltering(filtering.value);
    }

    public void OnChangeSorting(int index)
    {
        slotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        slotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }
}
