using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizationTest))]
public class LocalizationTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var text = target as LocalizationTest;
        var newId = EditorGUILayout.TextField("String ID", text.stringId);
        var newLang = (Languages)EditorGUILayout.EnumPopup("Language", text.editorLang);
        
        if(newId != text.stringId || newLang != text.editorLang)
        {
            text.stringId = newId;
            text.editorLang = newLang;
            text.OnChangeLanguage(text.editorLang);
            EditorUtility.SetDirty(text);
        }        
    }
}
