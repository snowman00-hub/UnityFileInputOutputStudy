﻿using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = 
        new Dictionary<string, DataTable>();

    static DataTableManager()
    {
        Init();
    }

    private static void Init()
    {
#if UNITY_EDITOR
        foreach(var fileName in DataTableIds.StringTableIds)
        {
            var table = new StringTable();
            table.Load(fileName);
            tables.Add(fileName, table);
        }
#else
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
#endif
        var itemTable = new ItemTable();
        itemTable.Load(DataTableIds.Item);
        tables.Add(DataTableIds.Item, itemTable);
    }

    public static StringTable StringTable
    {
        get
        {
            return Get<StringTable>(DataTableIds.String);
        }
    }

    public static ItemTable ItemTable
    {
        get
        {
            return Get<ItemTable>(DataTableIds.Item);
        }
    }

    public static T Get<T>(string tableId) where T : DataTable
    {
        if (!tables.ContainsKey(tableId))
        {
            Debug.LogError("테이블 없음");
            return null;
        }

        return tables[tableId] as T;
    }
}
