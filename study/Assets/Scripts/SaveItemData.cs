using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class SaveItemData
{
    public Guid instanceId;

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData itemData;
    public DateTime creationTime;

    public SaveItemData()
    {
        instanceId = Guid.NewGuid();
        creationTime = DateTime.Now;
    }
}
