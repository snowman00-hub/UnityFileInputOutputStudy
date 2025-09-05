using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using Mono.Cecil;
using UnityEngine;

public enum ItemTypes
{
    Weapon,
    Equip,
    Consumable,
}

public class ItemData
{
    public string Id { get; set; }
    public ItemTypes Type { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Value { get; set; }
    public int Cost { get; set; }
    public string Icon { get; set; }

    public override string ToString()
    {
        return $"{Id} / {Type} / {Name} / {Desc} / {Value} / {Cost} / {Icon}";
    }

    public string StringName => DataTableManger.StringTable.Get(Name);
    public string StringDesc => DataTableManger.StringTable.Get(Desc);
    
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");
}

public class ItemTable : DataTable
{
    private readonly Dictionary<string, ItemData> table = new Dictionary<string, ItemData>();

    public override void Load(string filename)
    {
        table.Clear();

        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<ItemData>(textAsset.text);
        foreach (var item in list)
        {
            if (!table.ContainsKey(item.Id))
            {
                table.Add(item.Id, item);
            }
            else
            {
                Debug.LogError("아이템 아이디 중복!");
            }
        }
    }

    public ItemData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            return null;
        }
        return table[id];
    }

    public ItemData GetRandom()
    {
        var itemList = table.Values.ToList();
        return itemList[Random.Range(0,itemList.Count)];
    }
}
