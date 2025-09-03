using UnityEngine;

public struct Tag
{
    public static readonly string Cube = "Cube";
    public static readonly string Save = "SaveObject";
}

public enum Languages
{
    Korean,
    English,
    Japanese
}

public static class DataTableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp",
    };

    public static readonly string Item = "ItemTableKr";

    public static string String => StringTableIds[(int)Variables.Language];
}

public static class Variables
{
    public static Languages Language = Languages.Korean;
}