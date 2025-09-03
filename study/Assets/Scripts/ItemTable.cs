using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    Equipment
}

public class ItemInfo
{
    public string IconFilePath { get; set; }
    public ItemType ItemType { get; set; }
    public string Value { get; set; }
    public string Price { get; set; }
    public string Description { get; set; }
}

public class ItemTable : DataTable
{
    public class Data
    {
        public string Id { get; set; }
        public ItemInfo Info { get; set; }
    }

    private readonly Dictionary<string, ItemInfo> dictionary = new Dictionary<string, ItemInfo>();

    public override void Load(string fileName)
    {
        dictionary.Clear();

        var path = string.Format(FormatPath, fileName);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<Data>(textAsset.text);

        foreach (var item in list)
        {
            if (!dictionary.ContainsKey(item.Id))
            {
                dictionary.Add(item.Id, item.Info);
            }
            else
            {
                Debug.LogError($"키 중복: {item.Id}");
            }
        }
    }
    public ItemInfo Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            return null;
        }

        return dictionary[key];
    }
}

public class ItemInfoMap : ClassMap<ItemInfo>
{
    public ItemInfoMap()
    {
        Map(m => m.IconFilePath).Name("IconFilePath");
        Map(m => m.ItemType).Name("ItemType");
        Map(m => m.Value).Name("Value");
        Map(m => m.Price).Name("Price");
        Map(m => m.Description).Name("Description");
    }
}

public class ItemDataMap : ClassMap<ItemTable.Data>
{
    public ItemDataMap()
    {
        Map(m => m.Id).Name("Id");
        References<ItemInfoMap>(m => m.Info);
    }
}